using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Application.DataAccess.Contexts.Configurations;

public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> entity)
    {
        entity.ToTable("wishlists");

        entity.HasKey(w => w.Id);
        entity.Property(w => w.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        entity.Property(w => w.UserId)
            .HasColumnName("user_id");

        entity.HasOne(w => w.User)
            .WithOne(u => u.Wishlist)
            .HasForeignKey<Wishlist>(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasIndex(w => w.UserId).IsUnique();
    }
}