using Backend.API.Extensions;
using Backend.Application.Services.PickupPoints;
using Backend.Application.Services.PickupPoints.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Backend.API.Controllers;

[ApiController]
[Route("api/pickup_point")]
public class PickupPointController : ControllerBase
{
    private readonly PickupPointService _pickupPointService;

    public PickupPointController(PickupPointService pickupPointService)
    {
        _pickupPointService = pickupPointService;
    }

    [Authorize]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _pickupPointService.GetPointsAsync((int)userId);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpPatch("select")]
    public async Task<IActionResult> UpdateSelectedPoint(UpdateSelectedPointRequest request)
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _pickupPointService.UpdateSelectedPointAsync((int)userId, request);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpDelete("remove")]
    public async Task<IActionResult> RemovePoint(RemovePointRequest request)
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();
        
        var response = await _pickupPointService.RemovePointAsync((int)userId, request);
        return response.ToHttpResponse();
    }
}