using Backend.API.Extensions;
using Backend.Application.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> GetUser()
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _userService.GetUserAsync((int)userId);
        return response.ToHttpResponse();
    }
}