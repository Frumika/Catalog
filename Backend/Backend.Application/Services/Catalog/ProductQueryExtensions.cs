using Backend.Domain.Models;

namespace Backend.Application.Services.Catalog;

public static class ProductQueryExtensions
{
    public static IQueryable<Product> FilterByCategory(this IQueryable<Product> query, int? categoryId)
    {
        return categoryId is null ? query : query.Where(p => p.CategoryId == categoryId);
    }

    public static IQueryable<Product> FilterByWishlist(this IQueryable<Product> query, bool? isWishlist, int? userId)
    {
        if (isWishlist is not true || userId is null) return query;
        return query.Where(p => p.WishedProducts.Any(wp => wp.Wishlist.UserId == userId));
    }

    public static IQueryable<Product> FilterByPrice(this IQueryable<Product> query, int? minPrice, int? maxPrice)
    {
        if (minPrice is not null) query = query.Where(p => p.Price >= minPrice);
        if (maxPrice is not null) query = query.Where(p => p.Price <= maxPrice);
        return query;
    }

    public static IQueryable<Product> ApplySorting(this IQueryable<Product> query, ProductSortOrder sortOrder,
        bool? isWishlist, int? userId)
    {
        if (isWishlist is true && userId is not null)
        {
            return sortOrder switch
            {
                ProductSortOrder.Newest => query.OrderByDescending(p => p.WishedProducts
                    .Where(wp => wp.Wishlist.UserId == userId)
                    .Select(wp => wp.AddedAt)
                    .FirstOrDefault()),
                ProductSortOrder.Oldest => query.OrderBy(p => p.WishedProducts
                    .Where(wp => wp.Wishlist.UserId == userId)
                    .Select(wp => wp.AddedAt)
                    .FirstOrDefault()),
                ProductSortOrder.PriceAsc => query.OrderBy(p => p.Price),
                ProductSortOrder.PriceDesc => query.OrderByDescending(p => p.Price),
                ProductSortOrder.DiscountDesc => query.OrderByDescending(p => p.DiscountPercent),
                _ => query.OrderBy(p => p.Id)
            };
        }

        return query.OrderBy(p => p.Id);
    }
}