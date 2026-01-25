using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.DataAccess.Postgres.Contexts;

public class MainDbContext : DbContext
{
    private const string MoneyType = "numeric(10,2)";

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Maker> Makers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderedProduct> OrderedProducts { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasKey(user => user.Id);
            entity.Property(user => user.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(user => user.Login)
                .HasColumnName("login")
                .HasMaxLength(128)
                .IsRequired();

            entity.Property(user => user.HashPassword)
                .HasColumnName("hash_password")
                .HasMaxLength(256)
                .IsRequired();

            entity.HasIndex(user => user.Login).IsUnique();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(p => p.MakerId)
                .HasColumnName("maker_id")
                .IsRequired();

            entity.Property(p => p.CategoryId)
                .HasColumnName("category_id")
                .IsRequired();

            entity.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(64)
                .IsRequired();

            entity.Property(p => p.Description)
                .HasColumnName("description");

            entity.Property(p => p.Price)
                .HasColumnName("price")
                .HasColumnType(MoneyType)
                .HasDefaultValue(0)
                .IsRequired();

            entity.Property(p => p.Count)
                .HasColumnName("count")
                .HasDefaultValue(0)
                .IsRequired();

            entity.Property(p => p.ImageUrl)
                .HasColumnName("image_url");

            entity.HasOne(p => p.Maker)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.MakerId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(p => p.MakerId);
            entity.HasIndex(p => p.CategoryId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");

            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(c => c.Name)
                .HasColumnName("name")
                .HasMaxLength(32)
                .IsRequired();

            entity.HasIndex(c => c.Name).IsUnique();
        });

        modelBuilder.Entity<Maker>(entity =>
        {
            entity.ToTable("makers");

            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(m => m.Name)
                .HasColumnName("name")
                .HasMaxLength(128)
                .IsRequired();

            entity.Property(m => m.Description)
                .HasColumnName("description");

            entity.HasIndex(m => m.Name).IsUnique();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("orders");

            entity.HasKey(o => o.Id);
            entity.Property(o => o.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(o => o.Status)
                .HasColumnName("status")
                .HasConversion<string>()
                .HasMaxLength(16)
                .IsRequired();

            entity.Property(o => o.TotalPrice)
                .HasColumnName("total_price")
                .HasColumnType(MoneyType)
                .IsRequired();

            entity.Property(o => o.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            entity.Property(o => o.DeletionTime)
                .HasColumnName("deletion_time")
                .IsRequired();

            entity.Property(o => o.PaidAt)
                .HasColumnName("paid_at");

            entity.Property(o => o.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            entity.HasOne(o => o.User)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(o => o.UserId);
            entity.HasIndex(o => o.Status);
        });

        modelBuilder.Entity<OrderedProduct>(entity =>
        {
            entity.ToTable("ordered_products");

            entity.HasKey(o => o.Id);
            entity.Property(o => o.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(o => o.ProductPrice)
                .HasColumnName("product_price")
                .HasColumnType(MoneyType)
                .IsRequired();

            entity.Property(o => o.Quantity)
                .HasColumnName("quantity")
                .HasDefaultValue(0)
                .IsRequired();

            entity.Property(o => o.OrderId)
                .HasColumnName("order_id")
                .IsRequired();

            entity.Property(o => o.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            entity.HasOne(o => o.Order)
                .WithMany(o => o.OrderedProducts)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(o => o.Product)
                .WithMany(o => o.OrderedProducts)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(o => o.OrderId);
            entity.HasIndex(o => o.ProductId);
        });
    }
}