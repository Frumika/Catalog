using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Application.DataAccess.Contexts.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("users");

        entity.HasKey(user => user.Id);
        entity.Property(user => user.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        entity.Property(user => user.Email)
            .HasColumnName("email")
            .HasMaxLength(256)
            .IsRequired();

        entity.Property(user => user.Login)
            .HasColumnName("login")
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(user => user.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        entity.Property(user => user.LastLoginAt)
            .HasColumnName("last_login_at");

        entity.HasIndex(user => user.Email).IsUnique();
    }
}