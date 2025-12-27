using static Backend.Application.StatusCodes.UserStatusCode;
using Backend.Application.DTO.Requests;
using Backend.Application.DTO.Requests.User;
using Backend.Application.DTO.Responses;
using Backend.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;


    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _identityService.LoginAsync(request);
        return GetHttpCode(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var response = await _identityService.RegisterAsync(request);
        return GetHttpCode(response);
    }


    private IActionResult GetHttpCode(UserResponse response)
    {
        return response.Code switch
        {
            Success => Ok(response),
            UserAlreadyExists => Conflict(response),
            UserNotFound => NotFound(response),
            InvalidLogin or InvalidPassword => Unauthorized(response),
            UnknownError => StatusCode(StatusCodes.Status500InternalServerError, response),
            _ => BadRequest(response)
        };
    }
}