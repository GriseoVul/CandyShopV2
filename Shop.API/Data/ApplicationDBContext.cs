using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop.API.Entities.Category;
using Shop.API.Entities.Orders;
using Shop.API.Entities.Product;

namespace Shop.API.Data;

public class ApplicationDBContext(
    DbContextOptions<ApplicationDBContext> options
) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    // public DbSet<CategoryCharacteristic> CategoryCharacteristics { get; set; }
    // public DbSet<Order> Orders { get; set; }
    // public DbSet<Busket> OrderItems { get; set; }
    // public DbSet<BusketItem> OrderItemItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Characteristics)
            .WithOne(cc => cc.Category)
            .HasForeignKey(cc => cc.CategoryId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryName)
            .HasPrincipalKey(c => c.Name);

        base.OnModelCreating(modelBuilder);
    }

}
