using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Application.DataAccess.Contexts.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.ToTable("categories");

        entity.HasKey(c => c.Id);
        entity.Property(c => c.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        entity.Property(c => c.Name)
            .HasColumnName("name")
            .HasMaxLength(40)
            .IsRequired();

        entity.HasIndex(c => c.Name).IsUnique();
    }
}