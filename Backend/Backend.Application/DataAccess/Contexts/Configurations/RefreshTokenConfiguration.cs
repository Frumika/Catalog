using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Application.DataAccess.Contexts.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> entity)
    {
        entity.ToTable("refresh_token");

        entity.HasKey(us => us.Id);
        entity.Property(us => us.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        entity.Property(us => us.Token)
            .HasColumnName("token")
            .IsRequired();

        entity.Property(rt => rt.ExpiresAt)
            .HasColumnName("expires_at")
            .IsRequired();

        entity.Property(rt => rt.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        entity.Property(rt => rt.IsRevoked)
            .HasColumnName("is_revoked")
            .HasDefaultValue(false)
            .IsRequired();

        entity.Property(us => us.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        entity.HasOne(us => us.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasIndex(us => us.UserId);
        entity.HasIndex(us => us.Token);
    }
}