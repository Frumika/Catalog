using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.DataAccess.Postgres.Contexts.Configurations;

public class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> entity)
    {
        entity.ToTable("sellers");

        entity.HasKey(m => m.Id);
        entity.Property(m => m.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        entity.Property(m => m.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(m => m.Description)
            .HasColumnName("description");

        entity.HasIndex(m => m.Name).IsUnique();
    }
}