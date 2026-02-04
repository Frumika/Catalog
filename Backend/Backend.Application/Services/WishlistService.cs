using Backend.Application.DTO.Entities.Wishlist;
using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Wishlist;
using Backend.Application.DTO.Responses;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Postgres.Contexts;
using Backend.DataAccess.Storages;
using Microsoft.EntityFrameworkCore;
using ResponseWishlistItem = Backend.Application.DTO.Entities.Wishlist.WishlistItem;
using WishlistItem = Backend.Domain.Models.WishlistItem;


namespace Backend.Application.Services;

public class WishlistService
{
    private readonly MainDbContext _dbContext;
    private readonly UserSessionStorage _userStorage;

    public WishlistService(MainDbContext dbContext, UserSessionStorage userStorage)
    {
        _dbContext = dbContext;
        _userStorage = userStorage;
    }

    public async Task<WishlistResponse> GetWishlistAsync(GetWishlistRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return WishlistResponse.Fail(WishlistStatusCode.BadRequest, validationResult.Message);

        try
        {
            int? userId = await _userStorage.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return WishlistResponse.Fail(WishlistStatusCode.UserNotFound, "The user session wasn't found");

            int wishlistId = await _dbContext.Wishlists
                .AsNoTracking()
                .Where(wi => wi.UserId == userId)
                .Select(wi => wi.Id)
                .FirstAsync();
           
            List<ResponseWishlistItem> wishlistItems = await _dbContext.WishlistItems
                .AsNoTracking()
                .Where(wi => wi.WishlistId == wishlistId)
                .Select(wi => new ResponseWishlistItem
                {
                    ProductId = wi.ProductId,
                    ProductName = wi.Product.Name,
                    ProductPrice = wi.Product.Price
                })
                .OrderBy(wi => wi.ProductId)
                .ToListAsync();

            return WishlistResponse.Success(new WishlistDto { WishlistItems = wishlistItems });
        }
        catch (Exception)
        {
            return WishlistResponse.Fail(WishlistStatusCode.UnknownError, "Internal server error");
        }
    }
    
    public async Task<WishlistResponse> AddProductAsync(AddProductRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return WishlistResponse.Fail(WishlistStatusCode.BadRequest, validationResult.Message);

        try
        {
            int? userId = await _userStorage.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return WishlistResponse.Fail(WishlistStatusCode.UserNotFound, "The user session wasn't found");

            await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);

            int wishlistId = await _dbContext.Wishlists
                .AsNoTracking()
                .Where(w => w.UserId == userId)
                .Select(w => w.Id)
                .FirstAsync();

            int? productId = await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Id == request.ProductId)
                .Select(p => (int?)p.Id)
                .FirstOrDefaultAsync();
            if (productId is null)
                return WishlistResponse.Fail(WishlistStatusCode.ProductNotFound, "The product wasn't found");

            bool isCartItemExists = await _dbContext.WishlistItems
                .AnyAsync(wi => wi.WishlistId == wishlistId && wi.ProductId == request.ProductId);
            if (!isCartItemExists)
            {
                _dbContext.WishlistItems.Add(
                    new WishlistItem
                    {
                        WishlistId = wishlistId,
                        ProductId = request.ProductId,
                        AddedAt = DateTime.UtcNow
                    }
                );
                await _dbContext.SaveChangesAsync();
            }

            return WishlistResponse.Success("The product was added");
        }
        catch (Exception)
        {
            return WishlistResponse.Fail(WishlistStatusCode.UnknownError, "Internal server error");
        }
    }
    
    public async Task<WishlistResponse> RemoveProductAsync(RemoveProductRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return WishlistResponse.Fail(WishlistStatusCode.BadRequest, validationResult.Message);

        try
        {
            int? userId = await _userStorage.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return WishlistResponse.Fail(WishlistStatusCode.UserNotFound, "User session wasn't found");

            await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);

            int wishlistId = await _dbContext.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstAsync();

            WishlistItem? cartItem = await _dbContext.WishlistItems
                .FirstOrDefaultAsync(wi => wi.WishlistId == wishlistId && wi.ProductId == request.ProductId);
            if (cartItem != null)
            {
                _dbContext.WishlistItems.Remove(cartItem);
                await _dbContext.SaveChangesAsync();
            }

            return WishlistResponse.Success("The product was deleted");
        }
        catch (Exception)
        {
            return WishlistResponse.Fail(WishlistStatusCode.UnknownError, "Internal server error");
        }
    }
}