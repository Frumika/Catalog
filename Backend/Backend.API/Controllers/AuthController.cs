using Backend.API.Extensions;
using Backend.Application.Services.Auth;
using Backend.Application.Services.Auth.Requests;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("{sessionId}")]
    public async Task<IActionResult> GetUserSession([FromRoute] string sessionId)
    {
        var response = await _authService.GetUserSession(sessionId);
        return response.ToHttpResponse();
    }

    [HttpPost("send_code")]
    public async Task<IActionResult> SendCode([FromBody] SendCodeRequest request)
    {
        var response = await _authService.SendCodeAsync(request);
        return response.ToHttpResponse();
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequest request)
    {
        var response = await _authService.VerifyCodeAsync(request);
        return response.ToHttpResponse();
    }

    [HttpDelete("logout")]
    public async Task<IActionResult> LogoutSession([FromBody] LogoutRequest request)
    {
        var response = await _authService.LogoutSessionAsync(request);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpDelete("logout_all")]
    public async Task<IActionResult> LogoutAllSessions([FromBody] LogoutRequest request)
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _authService.LogoutAllSessionsAsync((int)userId);
        return response.ToHttpResponse();
    }
}