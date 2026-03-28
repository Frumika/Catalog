using Backend.API.Extensions;
using Backend.Application.DTO.Requests.Catalog;
using Backend.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace Backend.API.Controllers;

[ApiController]
[Route("api/catalog")]
public class CatalogController : ControllerBase
{
    private readonly CatalogService _catalogService;

    public CatalogController(CatalogService catalogService)
    {
        _catalogService = catalogService;
    }
    
    [HttpGet("category/list")]
    public async Task<IActionResult> GetCategoryList()
    {
        var response = await _catalogService.GetCategoryListAsync();
        return response.ToHttpResponse();
    }

    [HttpGet("product/{id}")]
    public async Task<IActionResult> GetProduct([FromRoute] int id)
    {
        var response = await _catalogService.GetProductByIdAsync(id);
        return response.ToHttpResponse();
    }

    [HttpPost("product/list")]
    public async Task<IActionResult> GetProductsList([FromBody] GetProductListRequest request)
    {
        var response = await _catalogService.GetProductListAsync(request);
        return response.ToHttpResponse();
    }
}