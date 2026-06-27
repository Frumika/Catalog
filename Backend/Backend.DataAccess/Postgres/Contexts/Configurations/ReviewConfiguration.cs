using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.DataAccess.Postgres.Contexts.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> entity)
    {
        entity.ToTable("reviews");

        entity.HasKey(r => r.Id);
        entity.Property(r => r.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        entity.Property(r => r.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        entity.Property(r => r.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        entity.Property(r => r.Score)
            .HasColumnName("score")
            .IsRequired();

        entity.Property(r => r.Text)
            .HasColumnName("text");

        entity.Property(r => r.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        entity.Property(r => r.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValue(null);

        entity.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasIndex(r => r.UserId);
        entity.HasIndex(r => r.ProductId);
        entity.HasIndex(r => new { r.UserId, r.ProductId }).IsUnique();
    }
}