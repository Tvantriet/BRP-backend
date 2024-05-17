using Microsoft.AspNetCore.Mvc;
using Business.Services;

namespace FietsRoute_Backend.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;
        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        [HttpGet("forecast")]
        public async Task<IActionResult> GetWeatherForecast(double lon, double lat)
        {
            try
            {
                var forecast = await _weatherService.GetWeatherForecastAsync(lon, lat, DateTime.Now);
                return Ok(forecast);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching weather forecast.");
            }
        }
    }
}
