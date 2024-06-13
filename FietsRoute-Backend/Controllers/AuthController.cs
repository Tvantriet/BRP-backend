using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Business.Services;
using Business;

public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        var token = await _authService.Login(new User() { Username = username, Password = password});

        if (token == null)
        {
            return Unauthorized(); 
        }

        return Ok(new { Token = token });
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]User user)
    {
        var response = await _authService.Register(user);
        return Ok(response);
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUser([FromBody]User user)
    {
        var success = await _authService.DeleteUser(user);

        if (!success)
        {
            return NotFound();
        }

        return Ok();
    }
}
