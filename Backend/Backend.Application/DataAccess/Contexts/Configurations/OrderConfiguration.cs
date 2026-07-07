using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Application.DataAccess.Contexts.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    private const string MoneyType = "numeric(10,2)";
    
    public void Configure(EntityTypeBuilder<Order> entity)
    {
        entity.ToTable("orders");

        entity.HasKey(o => o.Id);
        entity.Property(o => o.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        entity.Property(o => o.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(20)
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
    }
}