using Backend.Application.DTO.Requests.Cart;
using Backend.Application.DTO.Responses;
using Backend.Application.Services;
using Backend.Application.StatusCodes;
using Microsoft.AspNetCore.Mvc;


namespace Backend.API.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }


    [HttpPost("product/add")]
    public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
    {
        var response = await _cartService.AddProductAsync(request);
        return ToHttpResponse(response);
    }

    [HttpPatch("product/quantity/update")]
    public async Task<IActionResult> UpdateProductQuantityProduct([FromBody] UpdateProductQuantityRequest request)
    {
        var response = await _cartService.UpdateProductQuantityAsync(request);
        return ToHttpResponse(response);
    }

    [HttpDelete("product/remove")]
    public async Task<IActionResult> RemoveProduct([FromBody] RemoveProductRequest request)
    {
        var response = await _cartService.RemoveProductAsync(request);
        return ToHttpResponse(response);
    }

    private IActionResult ToHttpResponse(CartResponse response)
    {
        return response.Code switch
        {
            CartStatusCode.Success => Ok(response),

            CartStatusCode.CartStateNotFound => NotFound(response),
            CartStatusCode.UserSessionNotFound => NotFound(response),
            CartStatusCode.ProductNotFound => NotFound(response),

            CartStatusCode.BadRequest => BadRequest(response),

            CartStatusCode.CartStateNotCreated => StatusCode(StatusCodes.Status500InternalServerError, response),
            CartStatusCode.CartStateNotUpdated => StatusCode(StatusCodes.Status500InternalServerError, response),

            CartStatusCode.UnknownError => StatusCode(StatusCodes.Status500InternalServerError, response),

            _ => BadRequest(response)
        };
    }
}