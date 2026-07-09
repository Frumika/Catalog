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
    [HttpPost("make")]
    public async Task<IActionResult> MakeOrder([FromBody] MakeOrderRequest request)
    {
        var response = await _orderService.MakeOrderAsync(request);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpPost("pay")]
    public async Task<IActionResult> PayOrder([FromBody] PayOrderRequest request)
    {
        var response = await _orderService.PayOrderAsync(request);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpDelete("cancel")]
    public async Task<IActionResult> CancelOrder([FromBody] CancelOrderRequest request)
    {
        var response = await _orderService.CancelOrderAsync(request);
        return response.ToHttpResponse();
    }
}