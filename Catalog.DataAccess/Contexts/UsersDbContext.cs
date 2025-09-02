using Catalog.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DataAccess.Contexts;

public class UsersDbContext : DbContext
{
    private const int StringLength = 255;

    public DbSet<User> Users { get; set; }

    
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(user => user.Id);
            entity.Property(user => user.Id).ValueGeneratedOnAdd();

            entity.Property(user => user.Login).HasMaxLength(StringLength);
            entity.HasIndex(user => user.Login).IsUnique();

            entity.Property(user => user.Email).HasMaxLength(StringLength);
            entity.HasIndex(user => user.Email).IsUnique();

            entity.Property(user => user.Password).HasMaxLength(StringLength);
        });
    }
}