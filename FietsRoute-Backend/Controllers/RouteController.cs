using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace FietsRoute_Backend.Controllers
{
    public class RouteController : Controller
    {
        private readonly RouteServiceSimple _routeService;
        public RouteController(RouteServiceSimple routeService)
        {
            _routeService = routeService;
        }
        [HttpGet("route")]
        public async Task<IActionResult> GetRoute(int departureId, int destinationId)
        {
            try
            {
                var forecast = await _routeService.GetRoute(departureId, destinationId);
                return Ok(forecast);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching weather forecast.");
            }
        }
    }
}
