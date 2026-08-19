using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Application.DataAccess.Contexts.Configurations;

public class OrderPositionConfiguration : IEntityTypeConfiguration<OrderPosition>
{
    private const string MoneyType = "numeric(10,2)";

    public void Configure(EntityTypeBuilder<OrderPosition> entity)
    {
        entity.ToTable("order_positions");

        entity.HasKey(o => new { o.OrderId, o.ProductId });

        entity.Property(o => o.Price)
            .HasColumnName("price")
            .HasColumnType(MoneyType)
            .IsRequired();

        entity.Property(op => op.DeliveryDate)
            .HasColumnName("delivery_date")
            .IsRequired();

        entity.Property(op => op.DiscountPercent)
            .HasColumnName("discount_percent")
            .HasDefaultValue(0)
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
            .WithMany(o => o.OrderPositions)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(o => o.Product)
            .WithMany(o => o.OrderPositions)
            .HasForeignKey(o => o.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasIndex(o => o.OrderId);
        entity.HasIndex(o => o.ProductId);
    }
}