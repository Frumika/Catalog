using Backend.Application.DTO.Requests.Cart;
using Backend.Application.DTO.Responses;
using Backend.Application.Services.Interfaces;
using Backend.Application.StatusCodes;
using Microsoft.AspNetCore.Mvc;


namespace Backend.API.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }


    [HttpPost("add")]
    public async Task<IActionResult> AddProduct(AddProductRequest request)
    {
        var response = await _cartService.AddProductAsync(request);
        return ToHttpResponse(response);
    }

    [HttpPost("update")]
    public async Task<IActionResult> HandleProduct(UpdateProductQuantityRequest request)
    {
        var response = await _cartService.UpdateProductQuantityAsync(request);
        return ToHttpResponse(response);
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveProduct(RemoveProductRequest request)
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