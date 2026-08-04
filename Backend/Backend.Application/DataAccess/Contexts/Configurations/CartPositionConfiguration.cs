using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Application.DataAccess.Contexts.Configurations;

public class CartPositionConfiguration : IEntityTypeConfiguration<CartPosition>
{
    public void Configure(EntityTypeBuilder<CartPosition> entity)
    {
        entity.ToTable("cart_positions");

        entity.HasKey(ci => new { ci.CartId, ci.ProductId });

        entity.Property(ci => ci.CartId)
            .HasColumnName("cart_id")
            .IsRequired();

        entity.Property(ci => ci.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        entity.Property(ci => ci.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        entity.Property(ci => ci.AddedAt)
            .HasColumnName("added_at")
            .IsRequired();

        entity.HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(ci => ci.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasIndex(ci => ci.CartId);
        entity.HasIndex(ci => ci.ProductId);
    }
}