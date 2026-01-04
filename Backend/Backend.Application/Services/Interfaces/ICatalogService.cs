using Backend.Application.DTO.Requests.Catalog;
using Backend.Application.DTO.Responses;


namespace Backend.Application.Services.Interfaces;

public interface ICatalogService
{
    Task<CatalogResponse> GetCategoryListAsync();
    Task<CatalogResponse> GetProductByIdAsync(int id);
    Task<CatalogResponse> GetProductListAsync(GetProductListRequest request);
}