using Catalog.Application.DTO.Requests;
using Catalog.Application.DTO.Responses;

namespace Catalog.Application.Interfaces;

public interface IProductService
{
    Task<GetProductsResponse> GetProductsAsync(GetProductsRequest request);
    Task<GetCategoriesResponse> GetAllCategoriesAsync();
}