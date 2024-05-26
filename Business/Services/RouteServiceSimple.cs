using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class RouteServiceSimple
    {
        private readonly DefaultContext _context;

        public RouteServiceSimple(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Route> GetRoute(int depId, int desId)
        {
            var connection = await _context.Connections
                .Include(c => c.Departure)
                .Include(c => c.Destination)
                .Where(c => c.DepartureCityId == depId && c.DestinationCityId == desId)
                .OrderBy(c => c.Duration)
                .FirstOrDefaultAsync();

            if (connection == null)
            {
                throw new KeyNotFoundException("Connection not found");
            }

            var route = new Route
            {
                Departure = new City
                {
                    Id = connection.DepartureCityId,
                    Name = connection.Departure.Name,
                    Latitude = connection.Departure.Latitude,
                    Longitude = connection.Departure.Longitude
                },
                Destination = new City
                {
                    Id = connection.DestinationCityId,
                    Name = connection.Destination.Name,
                    Latitude = connection.Destination.Latitude,
                    Longitude = connection.Destination.Longitude
                },
                Distance = connection.Distance,
                Duration = connection.Duration,
                Direction = connection.Direction
            };

            return route;
        }
    }
}
