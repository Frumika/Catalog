using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Application.DataAccess.Contexts.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> entity)
    {
        entity.ToTable("product_images");

        entity.HasKey(pi => pi.Id);
        entity.Property(pi => pi.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        entity.Property(pi => pi.Position)
            .HasColumnName("position")
            .IsRequired();

        entity.Property(pi => pi.Path)
            .HasColumnName("path")
            .IsRequired();

        entity.Property(pi => pi.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        entity.HasOne(pi => pi.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasIndex(pi => new { pi.ProductId, pi.Position }).IsUnique();
        entity.HasIndex(pi => pi.ProductId);
    }
}