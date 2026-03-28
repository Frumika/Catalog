using Backend.API.Extensions;
using Backend.Application.Requests.Wishlist;
using Backend.Application.Services;
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

    [HttpPost("get")]
    public async Task<IActionResult> GetWishlist([FromBody] GetWishlistRequest request)
    {
        var response = await _wishlistService.GetWishlistAsync(request);
        return response.ToHttpResponse();
    }

    [HttpPost("product/add")]
    public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
    {
        var response = await _wishlistService.AddProductAsync(request);
        return response.ToHttpResponse();
    }

    [HttpDelete("product/remove")]
    public async Task<IActionResult> RemoveProduct([FromBody] RemoveProductRequest request)
    {
        var response = await _wishlistService.RemoveProductAsync(request);
        return response.ToHttpResponse();
    }
}