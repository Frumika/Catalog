using Backend.API.Extensions;
using Backend.Application.Services.Orders;
using Backend.Application.Services.Orders.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Backend.API.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [Authorize]
    [HttpGet("{orderId:int}")]
    public async Task<IActionResult> GetByIdAsync(int orderId)
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _orderService.GetByIdAsync((int)userId, orderId);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpPost("make")]
    public async Task<IActionResult> MakeOrder([FromBody] MakeOrderRequest request)
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _orderService.MakeOrderAsync((int)userId, request);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpPost("pay/{orderId:int}")]
    public async Task<IActionResult> PayOrder([FromRoute] int orderId)
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _orderService.PayOrderAsync((int)userId, orderId);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpDelete("cancel/{orderId:int}")]
    public async Task<IActionResult> CancelOrder([FromRoute] int orderId)
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _orderService.CancelOrderAsync((int)userId, orderId);
        return response.ToHttpResponse();
    }
}