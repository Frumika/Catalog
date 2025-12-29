using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.DataAccess.Postgres.Contexts;

public class MainDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Maker> Makers { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasKey(user => user.Id);
            entity.Property(user => user.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(user => user.Login)
                .HasColumnName("login")
                .HasMaxLength(128)
                .IsRequired();

            entity.Property(user => user.HashPassword)
                .HasColumnName("hash_password")
                .HasMaxLength(256)
                .IsRequired();

            entity.HasIndex(user => user.Login).IsUnique();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(p => p.MakerId)
                .HasColumnName("maker_id");

            entity.Property(p => p.CategoryId)
                .HasColumnName("category_id");

            entity.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(64)
                .IsRequired();

            entity.Property(p => p.Description)
                .HasColumnName("description");

            entity.Property(p => p.Price)
                .HasColumnName("price")
                .HasColumnType("numeric(10,2)")
                .HasDefaultValue(0)
                .IsRequired();

            entity.Property(p => p.Count)
                .HasColumnName("count");

            entity.Property(p => p.ImageUrl)
                .HasColumnName("image_url");

            entity.HasOne(p => p.Maker)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.MakerId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(p => p.MakerId);
            entity.HasIndex(p => p.CategoryId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");

            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(c => c.Name)
                .HasColumnName("name")
                .HasMaxLength(32)
                .IsRequired();

            entity.HasIndex(c => c.Name).IsUnique();
        });

        modelBuilder.Entity<Maker>(entity =>
        {
            entity.ToTable("makers");

            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(m => m.Name)
                .HasColumnName("name")
                .HasMaxLength(128)
                .IsRequired();

            entity.Property(m => m.Description)
                .HasColumnName("description");
        });
    }
}