using Catalog.Application.DTO.Entities;
using Catalog.Application.DTO.Requests;
using Catalog.Application.DTO.Responses;
using Catalog.Application.Enums;
using Catalog.Application.Interfaces;
using Catalog.DataAccess.Contexts;
using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services;

public class ProductsService : IProductService
{
    private readonly ProductsDbContext _productsDbContext;

    public ProductsService(ProductsDbContext productsDbContext)
    {
        _productsDbContext = productsDbContext;
    }

    public async Task<GetProductsResponse> GetProductsAsync(GetProductsRequest request)
    {
        var validationResult = ValidateGetProductsRequest(request);
        if (validationResult is not null) return validationResult;

        var query = _productsDbContext.Products.AsNoTracking();
        
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
            var categories = await _productsDbContext.Categories
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