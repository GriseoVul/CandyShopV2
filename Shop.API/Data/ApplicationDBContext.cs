using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop.API.Entities.Category;
using Shop.API.Entities.Product;

namespace Shop.API.Data;

public class ApplicationDBContext(
    DbContextOptions<ApplicationDBContext> options
) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        base.OnModelCreating(modelBuilder);
    }

}
