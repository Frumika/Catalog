using Backend.Application.Common;
using Backend.Application.Common.Base;
using Backend.Application.Common.Statuses;
using Backend.Application.Services.Wishlists.Dtos;
using Backend.Application.Services.Wishlists.Requests;
using Backend.Domain.Models;

namespace Backend.Application.Services.Wishlists;

public class WishlistService
{
    private readonly IBaseRepository _baseRepository;
    private readonly IWishlistRepository _wishlistRepository;

    public WishlistService(IBaseRepository baseRepository, IWishlistRepository dbContext)
    {
        _baseRepository = baseRepository;
        _wishlistRepository = dbContext;
    }

    public async Task<Response> GetWishlistAsync(GetWishlistRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int? userId = await _baseRepository.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            WishlistDto wishlist = await _wishlistRepository.GetWishlistAsync(userId.Value);

            return Response.Success(wishlist);
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> AddProductAsync(AddProductRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int? userId = await _baseRepository.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user session wasn't found");

            int wishlistId =
                await _wishlistRepository.GetWishlistIdAsync(userId.Value);

            bool exists =
                await _wishlistRepository.IsProductExistsAsync(request.ProductId);

            if (!exists)
                return Response.Fail(new ProductNotFound(), "The product wasn't found");

            bool inWishlist =
                await _wishlistRepository.IsProductInWishlistAsync(
                    wishlistId,
                    request.ProductId);

            if (!inWishlist)
            {
                await _wishlistRepository.AddProductAsync(
                    wishlistId,
                    request.ProductId);

                await _baseRepository.CommitAsync();
            }

            return Response.Success("The product was added");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> RemoveProductAsync(RemoveProductRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int? userId = await _baseRepository.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            int wishlistId = await _wishlistRepository.GetWishlistIdAsync(userId.Value);
            WishlistItem? wishlistItem = await _wishlistRepository.GetWishlistItemAsync(wishlistId, request.ProductId);
            if (wishlistItem != null)
            {
                _baseRepository.Remove(wishlistItem);
                await _baseRepository.CommitAsync();
            }

            return Response.Success("The product was deleted");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }
}