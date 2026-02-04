using Backend.Application.DTO.Requests.Wishlist;
using Backend.Application.DTO.Responses;
using Backend.Application.Services;
using Backend.Application.StatusCodes;
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
        return ToHttpResponse(response);
    }

    [HttpPost("product/add")]
    public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
    {
        var response = await _wishlistService.AddProductAsync(request);
        return ToHttpResponse(response);
    }

    [HttpDelete("product/remove")]
    public async Task<IActionResult> RemoveProduct([FromBody] RemoveProductRequest request)
    {
        var response = await _wishlistService.RemoveProductAsync(request);
        return ToHttpResponse(response);
    }

    private IActionResult ToHttpResponse(WishlistResponse response)
    {
        return response.Code switch
        {
            WishlistStatusCode.Success => Ok(response),
            
            WishlistStatusCode.UserNotFound => NotFound(response),
            WishlistStatusCode.ProductNotFound => NotFound(response),

            WishlistStatusCode.BadRequest => BadRequest(response),

            WishlistStatusCode.UnknownError => StatusCode(StatusCodes.Status500InternalServerError, response),

            _ => BadRequest(response)
        };
    }
}