using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Application.DataAccess.Contexts.Configurations;

public class PickupPointConfiguration : IEntityTypeConfiguration<PickupPoint>
{
    public void Configure(EntityTypeBuilder<PickupPoint> entity)
    {
        entity.ToTable("pickup_points");

        entity.HasKey(pp => pp.Id);
        entity.Property(pp => pp.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        entity.Property(pp => pp.City)
            .HasColumnName("city")
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(pp => pp.StreetType)
            .HasColumnName("street_type")
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        entity.Property(pp => pp.StreetName)
            .HasColumnName("street_name")
            .HasMaxLength(150)
            .IsRequired();

        entity.Property(pp => pp.House)
            .HasColumnName("house")
            .HasMaxLength(30)
            .IsRequired();

        entity.Property(pp => pp.Building)
            .HasColumnName("building")
            .HasMaxLength(30);

        entity.Property(pp => pp.ShelfLifetime)
            .HasColumnName("shelf_lifetime")
            .IsRequired();

        entity.Property(pp => pp.AddedAt)
            .HasColumnName("added_at")
            .IsRequired();
    }
}