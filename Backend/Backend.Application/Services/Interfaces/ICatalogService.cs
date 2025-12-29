using Backend.Application.DTO.Requests.Product;
using Backend.Application.DTO.Responses;


namespace Backend.Application.Services.Interfaces;

public interface ICatalogService
{
    Task<ProductResponse> GetCategoryListAsync();
    Task<ProductResponse> GetProductByIdAsync(int id);
    Task<ProductResponse> GetProductListAsync(GetProductListRequest request);
}