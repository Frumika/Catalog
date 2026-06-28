using Backend.API.Extensions;
using Backend.Application.Requests.PickupPoint;
using Backend.Application.Services;
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

    [HttpGet("all")]
    public async Task<IActionResult> GetAll(GetPickupPointsRequest request)
    {
        var response = await _pickupPointService.GetPointsAsync(request);
        return response.ToHttpResponse();
    }

    [HttpPatch("select")]
    public async Task<IActionResult> UpdateSelectedPoint(UpdateSelectedPointRequest request)
    {
        var response = await _pickupPointService.UpdateSelectedPointAsync(request);
        return response.ToHttpResponse();
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemovePoint(RemovePointRequest request)
    {
        var response = await _pickupPointService.RemovePointAsync(request);
        return response.ToHttpResponse();
    }
}