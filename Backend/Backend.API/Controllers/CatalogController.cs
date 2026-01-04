using Backend.Application.DTO.Requests.Catalog;
using Backend.Application.DTO.Responses;
using Backend.Application.Services.Interfaces;
using Backend.Application.StatusCodes;
using Microsoft.AspNetCore.Mvc;


namespace Backend.API.Controllers;

[ApiController]
[Route("api/catalog")]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet("product/{id}")]
    public async Task<IActionResult> GetProduct([FromRoute] int id)
    {
        var response = await _catalogService.GetProductByIdAsync(id);
        return ToHttpResponse(response);
    }

    [HttpPost("product/list")]
    public async Task<IActionResult> GetProductsList([FromBody] GetProductListRequest request)
    {
        var response = await _catalogService.GetProductListAsync(request);
        return ToHttpResponse(response);
    }

    [HttpPost("category/list")]
    public async Task<IActionResult> GetCategoryList()
    {
        var response = await _catalogService.GetCategoryListAsync();
        return ToHttpResponse(response);
    }


    private IActionResult ToHttpResponse(CatalogResponse response)
    {
        return response.Code switch
        {
            CatalogStatusCode.Success => Ok(response),
            CatalogStatusCode.ProductNotFound or CatalogStatusCode.MakerNotFound => NotFound(response),
            CatalogStatusCode.IncorrectCategory or CatalogStatusCode.IncorrectMaker => BadRequest(response),
            CatalogStatusCode.BadRequest => BadRequest(response),
            CatalogStatusCode.UnknownError => StatusCode(StatusCodes.Status500InternalServerError, response),
            _ => BadRequest(response)
        };
    }
}