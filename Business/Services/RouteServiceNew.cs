using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Business.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class RouteServiceNew
    {
        private readonly DefaultContext _context;

        public RouteServiceNew(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Route> GetRoute(int depId, int desId)
        {
            var cities = await _context.Cities.ToListAsync();
            var connections = await _context.Connections.Include(c => c.Departure).Include(c => c.Destination).ToListAsync();

            var graph = cities.ToDictionary(c => c.Id, c => new List<Connection>());
            foreach (var connection in connections)
            {
                //graph[connection.DepartureCityId].Add(connection);
            }

            var (routeConnections, duration) = FindShortestPath(depId, desId, graph);

            if (routeConnections == null || routeConnections.Count == 0)
            {
                throw new KeyNotFoundException("Connection not found");
            }

            var route = new Route
            {
                Departure = routeConnections.First().Departure,
                Destination = routeConnections.Last().Destination,
                Distance = routeConnections.Sum(c => c.Distance),
                Duration = duration,
                Direction = routeConnections.First().Direction
            };

            return route;
        }

        private (List<Connection>, double) FindShortestPath(int startId, int targetId, Dictionary<int, List<Connection>> graph)
        {
            var distances = new Dictionary<int, double>();
            var previous = new Dictionary<int, Connection>();
            var queue = new Utilities.PriorityQueue<int, double>();

            foreach (var node in graph.Keys)
            {
                distances[node] = double.MaxValue;
                queue.Enqueue(node, double.MaxValue);
            }

            distances[startId] = 0;
            queue.UpdatePriority(startId, 0);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current == targetId) break;

                foreach (var neighbor in graph[current])
                {
                    var alt = distances[current] + neighbor.Duration;
                    if (alt < distances[neighbor.Destination.Id])
                    {
                        distances[neighbor.Destination.Id] = alt;
                        previous[neighbor.Destination.Id] = neighbor;
                        queue.UpdatePriority(neighbor.Destination.Id, alt);
                    }
                }
            }

            var path = new List<Connection>();
            var totalDuration = distances[targetId];
            if (totalDuration == double.MaxValue)
            {
                return (null, double.MaxValue);
            }

            for (var at = targetId; previous.ContainsKey(at); at = previous[at].Departure.Id)
            {
                path.Insert(0, previous[at]);
            }

            return (path, totalDuration);
        }
    }
}