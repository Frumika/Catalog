using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.DataAccess.Postgres.Contexts.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("users");

        entity.HasKey(user => user.Id);
        entity.Property(user => user.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        entity.Property(user => user.Login)
            .HasColumnName("login")
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(user => user.HashPassword)
            .HasColumnName("hash_password")
            .HasMaxLength(256)
            .IsRequired();

        entity.HasIndex(user => user.Login).IsUnique();
    }
}