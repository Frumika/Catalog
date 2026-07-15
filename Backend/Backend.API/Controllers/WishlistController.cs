using Backend.API.Extensions;
using Backend.Application.Services.Wishlists;
using Backend.Application.Services.Wishlists.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Backend.API.Controllers;

[ApiController]
[Route("api/wishlist")]
public class WishlistController : ControllerBase
{
    private readonly WishlistService _wishlistService;

    public WishlistController(WishlistService wishlistService)
    {
        _wishlistService = wishlistService;
    }

    [Authorize]
    [HttpPost("preview")]
    public async Task<IActionResult> GetWishlistPreview()
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _wishlistService.GetWishlistPreviewAsync((int)userId);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpPost("product/add")]
    public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _wishlistService.AddProductAsync((int)userId, request);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpDelete("product/remove")]
    public async Task<IActionResult> RemoveProduct([FromBody] RemoveProductRequest request)
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _wishlistService.RemoveProductAsync((int)userId, request);
        return response.ToHttpResponse();
    }
}