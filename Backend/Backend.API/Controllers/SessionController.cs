using Backend.API.Extensions;
using Backend.Application.Services.Sessions;
using Backend.Application.Services.Sessions.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Backend.API.Controllers;

[ApiController]
[Route("api/session")]
public class SessionController : ControllerBase
{
    private readonly SessionService _sessionService;

    public SessionController(SessionService sessionService)
    {
        _sessionService = sessionService;
    }
    

    [HttpPost("send_code")]
    public async Task<IActionResult> SendCode([FromBody] SendCodeRequest request)
    {
        var response = await _sessionService.SendCodeAsync(request);
        return response.ToHttpResponse();
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequest request)
    {
        var response = await _sessionService.VerifyCodeAsync(request);
        return response.ToHttpResponse();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshRequest request)
    {
        var response = await _sessionService.RefreshAccessTokenAsync(request);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpDelete("logout")]
    public async Task<IActionResult> LogoutSession([FromBody] LogoutRequest request)
    {
        var response = await _sessionService.LogoutSessionAsync(request);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpDelete("logout_all")]
    public async Task<IActionResult> LogoutAllSessions()
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _sessionService.LogoutAllSessionsAsync((int)userId);
        return response.ToHttpResponse();
    }
}