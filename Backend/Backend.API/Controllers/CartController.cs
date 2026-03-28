using Backend.API.Extensions;
using Backend.Application.Requests.Cart;
using Backend.Application.Services;
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


    [HttpPost("get")]
    public async Task<IActionResult> GetCart([FromBody] GetCartRequest request)
    {
        var response = await _cartService.GetCartAsync(request);
        return response.ToHttpResponse();
    }

    [HttpDelete("clear")]
    public async Task<IActionResult> ClearCart([FromBody] DeleteCartRequest request)
    {
        var response = await _cartService.ClearCartAsync(request);
        return response.ToHttpResponse();
    }

    [HttpPost("product/add")]
    public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
    {
        var response = await _cartService.AddProductAsync(request);
        return response.ToHttpResponse();
    }

    [HttpPatch("product/quantity/update")]
    public async Task<IActionResult> UpdateProductQuantityProduct([FromBody] UpdateProductQuantityRequest request)
    {
        var response = await _cartService.UpdateProductQuantityAsync(request);
        return response.ToHttpResponse();
    }

    [HttpDelete("product/remove")]
    public async Task<IActionResult> RemoveProduct([FromBody] RemoveProductRequest request)
    {
        var response = await _cartService.RemoveProductAsync(request);
        return response.ToHttpResponse();
    }
    
}