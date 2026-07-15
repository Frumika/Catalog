using Backend.Domain.Models;

namespace Backend.Application.Services.Catalog;

public static class ProductQueryExtensions
{
    public static IQueryable<Product> FilterByCategory(this IQueryable<Product> query, int? categoryId)
    {
        if (categoryId is null)
            return query;

        return query.Where(product => product.CategoryId == categoryId);
    }

    public static IQueryable<Product> FilterByWishlist(this IQueryable<Product> query, bool? isWishlist, int? userId)
    {
        if (isWishlist is not true || userId is null) return query;

        return query.Where(p => p.WishedProducts.Any(wp => wp.Wishlist.UserId == userId));
    }

    public static IQueryable<Product> FilterByPrice(this IQueryable<Product> query, decimal? minPrice,
        decimal? maxPrice)
    {
        if (minPrice is not null)
            query = query.Where(p => p.Price >= minPrice);

        if (maxPrice is not null)
            query = query.Where(p => p.Price <= maxPrice);

        return query;
    }
}