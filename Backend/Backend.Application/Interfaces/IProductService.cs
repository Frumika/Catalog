using Backend.Application.DTO.Requests;
using Backend.Application.DTO.Responses;

namespace Backend.Application.Interfaces;

public interface IProductService
{
    Task<GetProductsResponse> GetProductsAsync(GetProductsRequest request);
    Task<GetCategoriesResponse> GetAllCategoriesAsync();
}