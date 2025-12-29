using Backend.Application.DTO.Requests.User;
using Backend.Application.DTO.Responses;
using Backend.Application.Services.Interfaces;
using Backend.Application.StatusCodes;
using Microsoft.AspNetCore.Mvc;


namespace Backend.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);
        return ToHttpResponse(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var response = await _authService.RegisterAsync(request);
        return ToHttpResponse(response);
    }

    [HttpDelete("logout")]
    public async Task<IActionResult> LogoutSession([FromBody] LogoutSessionRequest request)
    {
        var response = await _authService.LogoutSessionAsync(request);
        return ToHttpResponse(response);
    }

    [HttpDelete("logout/all/{id}")]
    public async Task<IActionResult> LogoutSession([FromRoute] int id)
    {
        var response = await _authService.LogoutAllSessionsAsync(id);
        return ToHttpResponse(response);
    }

    private IActionResult ToHttpResponse(AuthResponse response)
    {
        return response.Code switch
        {
            AuthStatusCode.Success => Ok(response),
            AuthStatusCode.InvalidLogin or AuthStatusCode.InvalidPassword => Unauthorized(response),
            AuthStatusCode.UserAlreadyExists => Conflict(response),
            AuthStatusCode.UserNotFound or AuthStatusCode.SessionNotFound => NotFound(response),
            AuthStatusCode.BadRequest => BadRequest(response),
            AuthStatusCode.UnknownError => StatusCode(StatusCodes.Status500InternalServerError, response),
            _ => BadRequest(response)
        };
    }
}