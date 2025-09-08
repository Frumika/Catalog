using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalog.DataAccess.Contexts;

public class ProductsDbContext : DbContext
{
    private const int StringLength = 255;

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Maker> Makers { get; set; }

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.LogTo(_ => { }, LogLevel.Warning);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Maker)
            .WithMany(m => m.Products)
            .HasForeignKey(p => p.MakerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.Property(p => p.Name).HasMaxLength(StringLength);
            entity.HasIndex(p => new { p.Name, p.MakerId }).IsUnique();

            entity.Property(p => p.Price).HasColumnType("money");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).ValueGeneratedOnAdd();

            entity.Property(c => c.Name).HasMaxLength(StringLength);
            entity.HasIndex(c => c.Name).IsUnique();
        });

        modelBuilder.Entity<Maker>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).ValueGeneratedOnAdd();

            entity.Property(m => m.Name).HasMaxLength(StringLength);
            entity.HasIndex(m => m.Name).IsUnique();
        });
    }
}