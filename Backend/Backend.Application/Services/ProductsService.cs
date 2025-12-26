using Backend.Application.DTO.Entities;
using Backend.Application.DTO.Requests;
using Backend.Application.DTO.Responses;
using Backend.Application.Enums;
using Backend.Application.Interfaces;
using Backend.DataAccess.Contexts;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Services;

public class ProductsService : IProductService
{
    private readonly MainDbContext _dbContext;

    public ProductsService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetProductsResponse> GetProductsAsync(GetProductsRequest request)
    {
        var validationResult = ValidateGetProductsRequest(request);
        if (validationResult is not null) return validationResult;

        var query = _dbContext.Products.AsNoTracking();
        
        if (request.CategoryId is not null)
            query = query.Where(p => p.CategoryId == request.CategoryId.Value);
        
        
        try
        {
            var totalCount = await query.CountAsync();
            if (totalCount == 0)
            {
                var code = request.CategoryId is not null
                    ? GetProductsStatus.WrongCategory
                    : GetProductsStatus.EmptyList;

                var message = request.CategoryId is not null
                    ? "There is no such category"
                    : "The list of products is empty";

                return GetProductsResponse.Fail(code, message);
            }


            var products = await query
                .OrderBy(p => p.Id)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new ProductDto(p))
                .ToListAsync();

            return GetProductsResponse.Success(products, totalCount);
        }
        catch (Exception)
        {
            return GetProductsResponse.Fail(GetProductsStatus.UnknownError, "Internal server error");
        }
    }

    public async Task<GetCategoriesResponse> GetAllCategoriesAsync()
    {
        try
        {
            var categories = await _dbContext.Categories
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Select(c => new CategoryDto(c))
                .ToListAsync();

            return GetCategoriesResponse.Success(categories);
        }
        catch (Exception)
        {
            return GetCategoriesResponse.Fail(GetCategoriesStatus.UnknownError, "Internal server error");
        }
    }

    private GetProductsResponse? ValidateGetProductsRequest(GetProductsRequest request)
    {
        if (request.Page <= 0)
            return GetProductsResponse.Fail(GetProductsStatus.InvalidValue, "Page must be greater than 0");

        if (request.PageSize <= 0)
            return GetProductsResponse.Fail(GetProductsStatus.InvalidValue, "PageSize must be greater than 0");
        return null;
    }
}