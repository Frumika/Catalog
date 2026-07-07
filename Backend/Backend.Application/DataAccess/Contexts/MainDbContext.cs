using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.DataAccess.Contexts;

public class MainDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserSession> UserSessions => Set<UserSession>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Seller> Sellers => Set<Seller>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderedProduct> OrderedProducts => Set<OrderedProduct>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Wishlist> Wishlists => Set<Wishlist>();
    public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();
    public DbSet<PickupPoint> PickupPoints => Set<PickupPoint>();
    public DbSet<UserPickupPoint> UserPickupPoints => Set<UserPickupPoint>();

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
    }
}