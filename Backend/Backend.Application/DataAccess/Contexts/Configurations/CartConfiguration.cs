using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Application.DataAccess.Contexts.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> entity)
    {
        entity.ToTable("carts");

        entity.HasKey(c => c.Id);
        entity.Property(c => c.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        entity.Property(c => c.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        entity.HasOne(c => c.User)
            .WithOne(u => u.Cart)
            .HasForeignKey<Cart>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasIndex(c => c.UserId).IsUnique();
    }
}