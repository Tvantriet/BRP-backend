using Business;
using Business.Services;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Tests
{
    [TestFixture]
    public class CityServiceTests
    {
        private CityService _cityService;
        private DefaultContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DefaultContext(options);
            _cityService = new CityService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task CreateCity_ShouldAddCity()
        {
            var city = new City { Name = "Test City", Latitude = 12.34, Longitude = 56.78 };
            await _cityService.CreateCity(city);
            var result = await _context.Cities.FirstOrDefaultAsync(c => c.Name == "Test City");
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo("Test City"));
        }

        [Test]
        public async Task GetCities_ShouldReturnCities()
        {
            var city = new City { Name = "Test City", Latitude = 12.34, Longitude = 56.78 };
            await _cityService.CreateCity(city);

            var cities = await _cityService.GetCities();

            Assert.IsNotNull(cities);
            Assert.IsTrue(cities.Any());
        }
    }
}
