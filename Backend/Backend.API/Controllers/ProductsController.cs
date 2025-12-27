using Backend.Application.DTO.Requests.Product;
using Backend.Application.DTO.Responses;
using Backend.Application.Services.Interfaces;
using Backend.Application.StatusCodes;
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

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        var response = await _productService.CreateProductAsync(request);
        return ToHttpResponse(response);
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetProduct([FromRoute] int id)
    {
        var response = await _productService.GetProductByIdAsync(id);
        return ToHttpResponse(response);
    }

    [HttpPost("get/list")]
    public async Task<IActionResult> GetProductsList([FromBody] GetProductListRequest request)
    {
        var response = await _productService.GetProductListAsync(request);
        return ToHttpResponse(response);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request)
    {
        var response = await _productService.UpdateProductAsync(request);
        return ToHttpResponse(response);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {
        var response = await _productService.DeleteProductAsync(id);
        return ToHttpResponse(response);
    }

    [HttpPost("categories/list")]
    public async Task<IActionResult> GetCategoriesList()
    {
        var response = await _productService.GetAllCategoriesAsync();
        return ToHttpResponse(response);
    }

    private IActionResult ToHttpResponse(ProductResponse response)
    {
        return response.Code switch
        {
            ProductStatusCode.Success => Ok(response),
            ProductStatusCode.NotFound => NotFound(response),
            ProductStatusCode.IncorrectCategory or ProductStatusCode.IncorrectMaker => BadRequest(response),
            ProductStatusCode.BadRequest => BadRequest(response),
            ProductStatusCode.UnknownError => StatusCode(StatusCodes.Status500InternalServerError, response),
            _ => BadRequest(response)
        };
    }
}