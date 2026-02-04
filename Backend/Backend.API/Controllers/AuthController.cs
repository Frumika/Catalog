using Backend.Application.DTO.Requests.Auth;
using Backend.Application.DTO.Responses;
using Backend.Application.Services;
using Backend.Application.StatusCodes;
using Microsoft.AspNetCore.Mvc;


namespace Backend.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var response = await _authService.RegisterAsync(request);
        return ToHttpResponse(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);
        return ToHttpResponse(response);
    }

    [HttpDelete("logout")]
    public async Task<IActionResult> LogoutSession([FromBody] LogoutRequest request)
    {
        var response = await _authService.LogoutSessionAsync(request);
        return ToHttpResponse(response);
    }

    [HttpDelete("logout_all")]
    public async Task<IActionResult> LogoutAllSessions([FromBody] LogoutRequest request)
    {
        var response = await _authService.LogoutAllSessionsAsync(request);
        return ToHttpResponse(response);
    }

    private IActionResult ToHttpResponse(AuthResponse response)
    {
        return response.Code switch
        {
            AuthStatusCode.Success => Ok(response),
            AuthStatusCode.InvalidPassword => Unauthorized(response),
            AuthStatusCode.UserAlreadyExists => Conflict(response),
            AuthStatusCode.UserNotFound => NotFound(response),
            AuthStatusCode.BadRequest => BadRequest(response),
            AuthStatusCode.UnknownError => StatusCode(StatusCodes.Status500InternalServerError, response),
            _ => BadRequest(response)
        };
    }
}