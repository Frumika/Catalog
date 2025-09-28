using Backend.Application.DTO.Requests;
using Backend.Application.Enums;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("products_list")]
    public async Task<IActionResult> GetProductsList([FromBody] GetProductsRequest request)
    {
        var response = await _productService.GetProductsAsync(request);

        return response.Code switch
        {
            GetProductsStatus.Success => Ok(response),
            GetProductsStatus.EmptyList => NotFound(response),
            GetProductsStatus.WrongCategory or GetProductsStatus.InvalidValue => BadRequest(response),
            GetProductsStatus.UnknownError => StatusCode(StatusCodes.Status500InternalServerError, response),
            _ => BadRequest(response)
        };
    }

    [HttpPost("categories_list")]
    public async Task<IActionResult> GetCategoriesList()
    {
        var response = await _productService.GetAllCategoriesAsync();

        return response.Code switch
        {
            GetCategoriesStatus.Success => Ok(response),
            GetCategoriesStatus.UnknownError => StatusCode(StatusCodes.Status500InternalServerError, response),
            _ => BadRequest(response)
        };
    }
}