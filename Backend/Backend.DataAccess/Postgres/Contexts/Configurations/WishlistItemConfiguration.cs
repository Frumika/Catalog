using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.DataAccess.Postgres.Contexts.Configurations;

public class WishlistItemConfiguration : IEntityTypeConfiguration<WishlistItem>
{
    public void Configure(EntityTypeBuilder<WishlistItem> entity)
    {
        entity.ToTable("wishlist_items");

        entity.HasKey(ci => new { ci.WishlistId, ci.ProductId });

        entity.Property(ci => ci.WishlistId)
            .HasColumnName("wishlist_id")
            .IsRequired();

        entity.Property(ci => ci.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        entity.Property(ci => ci.AddedAt)
            .HasColumnName("added_at")
            .IsRequired();

        entity.HasOne(wi => wi.Wishlist)
            .WithMany(w => w.WishlistItems)
            .HasForeignKey(wi => wi.WishlistId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(wi => wi.Product)
            .WithMany(p => p.WishlistItems)
            .HasForeignKey(wi => wi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasIndex(wi => wi.WishlistId);
        entity.HasIndex(wi => wi.ProductId);
    }
}