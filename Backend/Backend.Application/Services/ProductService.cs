using Backend.Application.DTO.Entities.Product;
using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Product;
using Backend.Application.DTO.Responses;
using Backend.Application.Services.Interfaces;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Contexts;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Application.Services;

public class ProductService : IProductService
{
    private readonly MainDbContext _dbContext;

    public ProductService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductResponse> CreateProductAsync(CreateProductRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return ProductResponse.Fail(ProductStatusCode.BadRequest, validationResult.Message);

        try
        {
            bool isCategoryExist = await _dbContext.Categories
                .AsNoTracking()
                .AnyAsync(category => category.Id == request.CategoryId);
            if (!isCategoryExist)
                return ProductResponse.Fail(ProductStatusCode.IncorrectCategory, "Category not found");

            bool isMakerExist = await _dbContext.Makers
                .AsNoTracking()
                .AnyAsync(maker => maker.Id == request.MakerId);
            if (!isMakerExist)
                return ProductResponse.Fail(ProductStatusCode.IncorrectMaker, "Maker not found");

            Product product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Count = request.Count,
                Description = request.Description,
                CategoryId = request.CategoryId,
                MakerId = request.MakerId
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return ProductResponse.Success("Product was created");
        }
        catch (Exception)
        {
            return ProductResponse.Fail(ProductStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<ProductResponse> GetProductByIdAsync(int id)
    {
        if (id <= 0) return ProductResponse.Fail(ProductStatusCode.BadRequest, "Id must be greater than 0");

        try
        {
            Product? product = await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
                return ProductResponse.Fail(ProductStatusCode.NotFound, "Product not found");

            return ProductResponse.Success(new ProductDto(product));
        }
        catch (Exception)
        {
            return ProductResponse.Fail(ProductStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<ProductResponse> GetProductListAsync(GetProductListRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return ProductResponse.Fail(ProductStatusCode.BadRequest, validationResult.Message);

        try
        {
            IQueryable<Product> query = _dbContext.Products.AsNoTracking();

            if (request.CategoryId is not null)
            {
                bool isCategoryExist = await _dbContext.Categories
                    .AnyAsync(category => category.Id == request.CategoryId);

                if (!isCategoryExist)
                    return ProductResponse.Fail(ProductStatusCode.IncorrectCategory, "Category not found");

                query = query.Where(product => product.CategoryId == request.CategoryId);
            }

            int totalCount = await query.CountAsync();

            List<ProductDto> products = await query
                .OrderBy(product => product.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(product => new ProductDto(product))
                .ToListAsync();

            return ProductResponse.Success(new ProductListDto(products, totalCount));
        }
        catch (Exception)
        {
            return ProductResponse.Fail(ProductStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<ProductResponse> UpdateProductAsync(UpdateProductRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return ProductResponse.Fail(ProductStatusCode.BadRequest, validationResult.Message);

        try
        {
            Product? product = _dbContext.Products.FirstOrDefault(p => p.Id == request.Id);
            if (product is null)
                return ProductResponse.Fail(ProductStatusCode.NotFound, "Product not found");

            product.Name = request.Name;
            product.Price = request.Price;
            product.Count = request.Count;
            product.Description = request.Description;

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            return ProductResponse.Success("Product was updated");
        }
        catch (Exception)
        {
            return ProductResponse.Fail(ProductStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<ProductResponse> DeleteProductAsync(int id)
    {
        if (id <= 0) return ProductResponse.Fail(ProductStatusCode.BadRequest, "Id must be greater than 0");

        try
        {
            Product? product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product is null)
                return ProductResponse.Fail(ProductStatusCode.NotFound, "Product not found");

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return ProductResponse.Success("Product was deleted");
        }
        catch (Exception)
        {
            return ProductResponse.Fail(ProductStatusCode.UnknownError, "Internal server error");
        }
    }
}