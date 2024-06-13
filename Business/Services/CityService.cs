using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business;

namespace Business.Services
{
    public class CityService
    {
        private readonly DefaultContext _context;

        public CityService(DefaultContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            var cities = await _context.Cities.ToListAsync();
            return cities.Select(c => new City
            {
                Id = c.Id,
                Name = c.Name,
                Latitude = c.Latitude,
                Longitude = c.Longitude
            });
        }

        public async Task<City> CreateCity(City city)
        {
            var entity = new Data.Entities.City
            {
                Name = city.Name,
                Latitude = city.Latitude,
                Longitude = city.Longitude
            };

            _context.Cities.Add(entity);
            await _context.SaveChangesAsync();

            city.Id = entity.Id;
            return city;
        }

        public async Task<City> UpdateCity(City city)
        {
            var entity = await _context.Cities.FindAsync(city.Id);

            if (entity == null)
            {
                return null;
            }

            entity.Name = city.Name;
            entity.Latitude = city.Latitude;
            entity.Longitude = city.Longitude;

            _context.Cities.Update(entity);
            await _context.SaveChangesAsync();

            return city;
        }

        public async Task<bool> DeleteCity(int id)
        {
            var entity = await _context.Cities.FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            _context.Cities.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Connection> AddConnection(Connection connection)
        {
            var entity = new Data.Entities.Connection
            {
                DepartureCityId = connection.Departure.Id,
                DestinationCityId = connection.Destination.Id,
                Distance = connection.Distance,
                Duration = connection.Duration,
                Direction = connection.Direction
            };

            _context.Connections.Add(entity);
            await _context.SaveChangesAsync();

            connection.Id = entity.Id;
            return connection;
        }

        public async Task<IEnumerable<Connection>> GetConnections()
        {
            var connections = await _context.Connections
                .Include(c => c.Departure)
                .Include(c => c.Destination)
                .ToListAsync();

            return connections.Select(c => new Connection
            {
                Id = c.Id,
                Departure = new City
                {
                    Id = c.Departure.Id,
                    Name = c.Departure.Name,
                    Latitude = c.Departure.Latitude,
                    Longitude = c.Departure.Longitude
                },
                Destination = new City
                {
                    Id = c.Destination.Id,
                    Name = c.Destination.Name,
                    Latitude = c.Destination.Latitude,
                    Longitude = c.Destination.Longitude
                },
                Distance = c.Distance,
                Duration = c.Duration,
                Direction = c.Direction
            });
        }

        public async Task<bool> DeleteConnection(int id)
        {
            var entity = await _context.Connections.FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            _context.Connections.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
