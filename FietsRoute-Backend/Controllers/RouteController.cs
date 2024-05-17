using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace FietsRoute_Backend.Controllers
{
    public class RouteController : Controller
    {
        private readonly RouteService _routeService;
        public RouteController(RouteService routeService)
        {
            _routeService = routeService;
        }
        [HttpGet("computeRoute")]
        public async Task<IActionResult> GetWeatherForecast(double sLat, double eLat, double sLong, double eLong)
        {
            try
            {
                var forecast = await _routeService.GetRouteAsync(sLat, sLong, eLat, eLong);
                return Ok(forecast);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching weather forecast.");
            }
        }
    }
}
