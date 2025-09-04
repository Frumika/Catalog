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


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var response = await _userService.RegisterAsync(request);
        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _userService.LoginAsync(request);
        return response.IsSuccess
            ? Ok(response)
            : Unauthorized(response);
    }
}