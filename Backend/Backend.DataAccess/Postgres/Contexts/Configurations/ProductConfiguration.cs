using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.DataAccess.Postgres.Contexts.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    private const string MoneyType = "numeric(10,2)";

    public void Configure(EntityTypeBuilder<Product> entity)
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
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(p => p.Description)
            .HasColumnName("description");

        entity.Property(p => p.Price)
            .HasColumnName("price")
            .HasColumnType(MoneyType)
            .IsRequired();

        entity.Property(p => p.DiscountPercent)
            .HasColumnName("discount_percent")
            .HasDefaultValue(0)
            .IsRequired();

        entity.Property(p => p.Quantity)
            .HasColumnName("quantity")
            .HasDefaultValue(0)
            .IsRequired();

        entity.HasOne(p => p.Seller)
            .WithMany(m => m.Products)
            .HasForeignKey(p => p.MakerId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasIndex(p => p.MakerId);
        entity.HasIndex(p => p.CategoryId);
    }
}