using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Application.DataAccess.Contexts.Configurations;

public class UserPickupPointConfiguration : IEntityTypeConfiguration<UserPickupPoint>
{
    public void Configure(EntityTypeBuilder<UserPickupPoint> entity)
    {
        entity.ToTable("user_pickup_points");

        entity.HasKey(upp => new { upp.UserId, upp.PickupPointId });

        entity.Property(upp => upp.UserId)
            .HasColumnName("user_id");

        entity.Property(upp => upp.PickupPointId)
            .HasColumnName("pickup_point_id");
        
        entity.Property(upp => upp.SelectedAt)
            .HasColumnName("selected_at")
            .IsRequired();
        
        entity.Property(upp => upp.AddedAt)
            .HasColumnName("added_at")
            .IsRequired();

        entity.HasOne(upp => upp.User)
            .WithMany(u => u.UserPickupPoints)
            .HasForeignKey(upp => upp.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(upp => upp.PickupPoint)
            .WithMany(pp => pp.UserPickupPoints)
            .HasForeignKey(upp => upp.PickupPointId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasIndex(upp => upp.PickupPointId);
    }
}