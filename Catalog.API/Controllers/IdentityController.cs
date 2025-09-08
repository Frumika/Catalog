using static Catalog.Application.Enums.IdentityResultCode;
using Catalog.Application.DTO;
using Catalog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

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


    private IActionResult GetHttpCode(IdentityResponse response)
    {
        return response.Code switch
        {
            Success => Ok(response),
            UserAlreadyExists => Conflict(response),
            UserNotFound => NotFound(response),
            InvalidEmail or InvalidLogin or InvalidPassword => Unauthorized(response),
            UnknownError => StatusCode(StatusCodes.Status500InternalServerError, response),
            _ => BadRequest(response)
        };
    }
}