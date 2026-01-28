using Backend.Application.DTO.Requests.Order;
using Backend.Application.DTO.Responses;
using Backend.Application.Services;
using Backend.Application.StatusCodes;
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
        return ToHttpResponse(response);
    }

    [HttpPost("pay")]
    public async Task<IActionResult> PayOrder([FromBody] PayOrderRequest request)
    {
        var response = await _orderService.PayOrderAsync(request);
        return ToHttpResponse(response);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> CancelOrder([FromBody] CancelOrderRequest request)
    {
        var response = await _orderService.CancelOrderAsync(request);
        return ToHttpResponse(response);
    }

    private IActionResult ToHttpResponse(OrderResponse response)
    {
        return response.Code switch
        {
            OrderStatusCode.Success => Ok(response),

            OrderStatusCode.BadRequest => BadRequest(response),
            OrderStatusCode.IncorrectQuantity => BadRequest(response),

            OrderStatusCode.UserSessionNotFound => NotFound(response),
            OrderStatusCode.CartStateNotFound => NotFound(response),
            OrderStatusCode.ProductNotFound => NotFound(response),
            OrderStatusCode.OrderNotFound => NotFound(response),

            OrderStatusCode.InvalidOrderStatus => Conflict(response),
            
            OrderStatusCode.UnknownError => StatusCode(StatusCodes.Status500InternalServerError, response),

            _ => BadRequest(response)
        };
    }
}