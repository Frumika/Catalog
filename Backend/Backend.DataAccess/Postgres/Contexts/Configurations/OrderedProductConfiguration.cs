using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.DataAccess.Postgres.Contexts.Configurations;

public class OrderedProductConfiguration : IEntityTypeConfiguration<OrderedProduct>
{
    private const string MoneyType = "numeric(10,2)";

    public void Configure(EntityTypeBuilder<OrderedProduct> entity)
    {
        entity.ToTable("ordered_products");

        entity.HasKey(o => new { o.OrderId, o.ProductId });

        entity.Property(o => o.ProductPrice)
            .HasColumnName("product_price")
            .HasColumnType(MoneyType)
            .IsRequired();

        entity.Property(o => o.Quantity)
            .HasColumnName("quantity")
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
    }
}