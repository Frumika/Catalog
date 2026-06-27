using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.DataAccess.Postgres.Contexts.Configurations;

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
            .HasMaxLength(30)
            .IsRequired();

        entity.Property(pp => pp.StreetName)
            .HasColumnName("street_name")
            .HasConversion<string>()
            .HasMaxLength(150)
            .IsRequired();

        entity.Property(pp => pp.House)
            .HasColumnName("house")
            .HasMaxLength(30)
            .IsRequired();

        entity.Property(pp => pp.Building)
            .HasColumnName("building")
            .HasMaxLength(30);
        
        entity.Property(pp => pp.AddedAt)
            .HasColumnName("created_at")
            .IsRequired();
    }
}