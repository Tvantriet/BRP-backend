using Microsoft.AspNetCore.Mvc;
using Business.Services;
using Business;
using Microsoft.AspNetCore.Authorization;

namespace FietsRoute_Backend.Controllers
{
    public class CityController : ControllerBase
    {
        private readonly CityService _cityService;
        public CityController(CityService cityService)
        {
            _cityService = cityService;
        }
        [HttpGet("cities")]
        public async Task<IActionResult> GetCities()
        {
            try
            {
                //var cities = await _cityService.GetCities();
                //creating list of dutch cities
                List<City> cities =
                [
                    new City() { Id = 1, Name = "Amsterdam", Latitude = 52.3676, Longitude = 4.9041 },
                    new City() { Id = 5, Name = "Rotterdam", Latitude = 51.9225, Longitude = 4.47917 },
                    new City() { Id = 8, Name = "Den Haag", Latitude = 52.0705, Longitude = 4.3007 },
                    new City() { Id = 4, Name = "Utrecht", Latitude = 52.0907, Longitude = 5.1214 },
                    new City() { Id = 3, Name = "Eindhoven", Latitude = 51.4416, Longitude = 5.4697 },
                    new City() { Id = 2, Name = "Tilburg", Latitude = 51.5555, Longitude = 5.0913 },
                ];
                return Ok(cities);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching cities.");
            }
        }
        //create
        [HttpPost("cities")]
        public async Task<IActionResult> CreateCity([FromBody] City city)
        {
            try
            {
                var newCity = await _cityService.CreateCity(city);
                return Ok(newCity);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating city.");
            }
        }
        //add connection 
        [HttpPost("connections")]
        public async Task<IActionResult> AddCityConnection([FromBody] Connection cityConnection)
        {
            try
            {
                var newCityConnection = await _cityService.AddConnection(cityConnection);
                return Ok(newCityConnection);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding city connection.");
            }
        }
        [HttpGet("connections")]
        public IActionResult GetCityConnections()
        {
            try
            {
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding city connection.");
            }
        }
        [Authorize]
        [HttpGet("krabby-patty-formula")]
        public IActionResult GetFormula()
        {
            string formula = "the secret formula";
            return Ok(formula);
        }
    }
}
