using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.DataAccess.Postgres.Contexts.Configurations;

public class MakerConfiguration : IEntityTypeConfiguration<Maker>
{
    public void Configure(EntityTypeBuilder<Maker> entity)
    {
        entity.ToTable("makers");

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