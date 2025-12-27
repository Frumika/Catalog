using Backend.Application.DTO.Entities.Category;
using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Category;
using Backend.Application.DTO.Responses;
using Backend.Application.Services.Interfaces;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Contexts;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly MainDbContext _dbContext;

    public CategoryService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return CategoryResponse.Fail(CategoryStatusCode.BadRequest, validationResult.Message);

        try
        {
            // Todo: Добавить индекс на название категории в бд
            Category category = new() { Name = request.Name };
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return CategoryResponse.Success("Category was created");
        }
        catch (Exception)
        {
            return CategoryResponse.Fail(CategoryStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<CategoryResponse> GetCategoryByIdAsync(int id)
    {
        if (id <= 0)
            return CategoryResponse.Fail(CategoryStatusCode.BadRequest, "Category id must be greater than 0");

        try
        {
            Category? category = await _dbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (category is null)
                return CategoryResponse.Fail(CategoryStatusCode.NotFound, "Category not found");

            return CategoryResponse.Success(new CategoryDto(category));
        }
        catch (Exception)
        {
            return CategoryResponse.Fail(CategoryStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<CategoryResponse> GetCategoryListAsync()
    {
        try
        {
            var categories = await _dbContext.Categories
                .AsNoTracking()
                .OrderBy(category => category.Id)
                .Select(category => new CategoryDto(category))
                .ToListAsync();

            return CategoryResponse.Success(new CategoryListDto(categories));
        }
        catch (Exception)
        {
            return CategoryResponse.Fail(CategoryStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<CategoryResponse> UpdateCategoryAsync(UpdateCategoryRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return CategoryResponse.Fail(CategoryStatusCode.BadRequest, validationResult.Message);

        try
        {
            Category? category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (category is null)
                return CategoryResponse.Fail(CategoryStatusCode.NotFound, "Category not found");

            category.Name = request.Name;

            _dbContext.Update(category);
            await _dbContext.SaveChangesAsync();

            return CategoryResponse.Success("Category was updated");
        }
        catch (Exception)
        {
            return CategoryResponse.Fail(CategoryStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<CategoryResponse> DeleteCategoryAsync(int id)
    {
        if (id <= 0)
            return CategoryResponse.Fail(CategoryStatusCode.BadRequest, "Category id must be greater than 0");

        try
        {
            Category? category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category is null)
                return CategoryResponse.Fail(CategoryStatusCode.NotFound, "Category not found");

            _dbContext.Remove(category);
            await _dbContext.SaveChangesAsync();
            
            return CategoryResponse.Success("Category was deleted");
        }
        catch (Exception)
        {
            return CategoryResponse.Fail(CategoryStatusCode.UnknownError, "Internal server error");
        }
    }
}