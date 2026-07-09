using Backend.API.Extensions;
using Backend.Application.Services.Carts;
using Backend.Application.Services.Carts.Requests;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost("preview")]
    public async Task<IActionResult> GetCartPreview()
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _cartService.GetCartPreviewAsync((int)userId);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpPost("get")]
    public async Task<IActionResult> GetCart()
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _cartService.GetCartAsync((int)userId);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpDelete("clear")]
    public async Task<IActionResult> ClearCart()
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _cartService.ClearCartAsync((int)userId);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpPost("product/add")]
    public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _cartService.AddProductAsync((int)userId, request);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpPatch("product/quantity/update")]
    public async Task<IActionResult> UpdateProductQuantityProduct([FromBody] UpdateProductQuantityRequest request)
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _cartService.UpdateProductQuantityAsync((int)userId, request);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpDelete("product/remove")]
    public async Task<IActionResult> RemoveProduct([FromBody] RemoveProductRequest request)
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _cartService.RemoveProductAsync((int)userId, request);
        return response.ToHttpResponse();
    }
}