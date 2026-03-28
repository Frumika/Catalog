using Backend.API.Extensions;
using Backend.Application.Requests.Order;
using Backend.Application.Services;
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


    [HttpPost("make")]
    public async Task<IActionResult> MakeOrder([FromBody] MakeOrderRequest request)
    {
        var response = await _orderService.MakeOrderAsync(request);
        return response.ToHttpResponse();
    }

    [HttpPost("pay")]
    public async Task<IActionResult> PayOrder([FromBody] PayOrderRequest request)
    {
        var response = await _orderService.PayOrderAsync(request);
        return response.ToHttpResponse();
    }

    [HttpDelete("cancel")]
    public async Task<IActionResult> CancelOrder([FromBody] CancelOrderRequest request)
    {
        var response = await _orderService.CancelOrderAsync(request);
        return response.ToHttpResponse();
    }
}