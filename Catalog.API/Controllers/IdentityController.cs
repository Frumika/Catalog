using static Catalog.Application.Enums.UserStatusCode;
using Catalog.Application.DTO;
using Catalog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    private readonly IUserService _userService;


    public IdentityController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _userService.LoginAsync(request);
        return GetHttpCode(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var response = await _userService.RegisterAsync(request);
        return GetHttpCode(response);
    }


    private IActionResult GetHttpCode(UserResponse response)
    {
        return response.Code switch
        {
            Logged => Ok(response),
            Registered => StatusCode(StatusCodes.Status201Created, response),
            UserAlreadyExists => Conflict(response),
            UserNotFound => NotFound(response),
            InvalidCredentials => Unauthorized(response),
            UnknownError => StatusCode(StatusCodes.Status500InternalServerError, response),
            _ => BadRequest(response)
        };
    }
}