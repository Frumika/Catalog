using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Application.DataAccess.Contexts.Configurations;

public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> entity)
    {
        entity.ToTable("user_sessions");

        entity.HasKey(us => us.Id);
        entity.Property(us => us.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        entity.Property(us => us.UId)
            .HasColumnName("uid")
            .IsRequired();

        entity.Property(us => us.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        entity.Property(us => us.OrderId)
            .HasColumnName("order_id")
            .HasDefaultValue(null);

        entity.HasOne(us => us.User)
            .WithMany(u => u.UserSessions)
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(us => us.PendingOrder)
            .WithOne(o => o.UserSession)
            .HasForeignKey<UserSession>(us => us.OrderId)
            .OnDelete(DeleteBehavior.SetNull);

        entity.HasIndex(us => us.UserId);
        entity.HasIndex(us => us.OrderId).IsUnique();
        entity.HasIndex(us => us.UId).IsUnique();
    }
}