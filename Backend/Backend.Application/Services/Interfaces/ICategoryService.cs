using Backend.Application.DTO.Requests.Category;
using Backend.Application.DTO.Responses;

namespace Backend.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest request);
    Task<CategoryResponse> GetCategoryByIdAsync(int id);
    Task<CategoryResponse> GetCategoryListAsync();
    Task<CategoryResponse> UpdateCategoryAsync(UpdateCategoryRequest request);
    Task<CategoryResponse> DeleteCategoryAsync(int id);
}