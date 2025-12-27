using Backend.Application.DTO.Requests.Product;
using Backend.Application.DTO.Responses;


namespace Backend.Application.Services.Interfaces;

public interface IProductService
{
    Task<ProductResponse> CreateProductAsync(CreateProductRequest request);
    Task<ProductResponse> GetProductByIdAsync(int id);
    Task<ProductResponse> GetProductListAsync(GetProductListRequest request);
    Task<ProductResponse> UpdateProductAsync(UpdateProductRequest request);
    Task<ProductResponse> DeleteProductAsync(int id);
}