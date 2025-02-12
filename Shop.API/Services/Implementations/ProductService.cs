using System;

namespace Shop.API.Services.Implementations;

using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Shop.API.Data;
using Shop.API.Entities.Product;
using Shop.API.Services.Interfaces;
public class ProductService 
(
    ApplicationDBContext context,
    ILogger<ProductService> logger,
    IMemoryCache cache
): IProductService
{
    private readonly ApplicationDBContext _context = context;
    private readonly IMemoryCache _cache = cache;
    private readonly ILogger<ProductService> _logger = logger;

    public async Task<Product> CreateAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> UpdateAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> DeleteAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        if(_cache.TryGetValue(id, out Product? product))
        {
            _logger.LogInformation("Product {id} found in cache", id);
            return product;
        }

        try
        {
            product = await _context.Products.FindAsync(id);
            if(product != null)
            {
                _logger.LogInformation("Product {id} found in database", id);
                _cache.Set(id, product, TimeSpan.FromMinutes(5));
                return product;
            }
            _logger.LogWarning("Product {id} not found", id);
            throw new KeyNotFoundException($"Product {id} not found");
        }
        catch( Exception e)
        {
            _logger.LogError(e, "Error while getting product {id}", id);
            throw ;
        }
    }

    public async Task<IQueryable<Product>> GetAllAsync()
    {
        if(_cache.TryGetValue("AllProducts", out IQueryable<Product>? products))
        {
            _logger.LogInformation("AllProducts found in cache");
            return products;
        }

        try
        {
            products = _context.Products;
            _logger.LogInformation("AllProducts found in database");
            _cache.Set("AllProducts", products, TimeSpan.FromMinutes(5));
            return products;
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error while getting AllProducts");
            throw;
        }
    }

    public async Task<IQueryable<Product>> GetByConditionAsync(Expression<Func<Product, bool>> expression)
    {
        string CacheKey = expression.Print();

        if(_cache.TryGetValue(CacheKey, out IQueryable<Product>? products))
        {
            _logger.LogInformation("Products found in cache");
            return products;
        }

        try
        {
            products = _context.Products.Where(expression);
            _logger.LogInformation("Products found in database");
            _cache.Set(CacheKey, products, TimeSpan.FromMinutes(5));
            return products;
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error while getting products");
            throw;
        }
    }

    public async Task<int> CountAsync()
    {
        if(_cache.TryGetValue("ProductCount", out int count))
        {
            _logger.LogInformation("ProductCount found in cache");
            return count;
        }

        try
        {
            count = await _context.Products.CountAsync();
            _logger.LogInformation("ProductCount found in database");
            _cache.Set("ProductCount", count, TimeSpan.FromMinutes(5));
            return count;
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error while getting count");
            throw;
        }
    }

    public async Task<bool> ExistAsync(Expression<Func<Product, bool>> expression)
    {
        string CacheKey = expression.Print();

        if(_cache.TryGetValue(CacheKey, out bool exist))
        {
            _logger.LogInformation("Exist found in cache");
            return exist;
        }

        try
        {
            exist = await _context.Products.AnyAsync(expression);
            _logger.LogInformation("Exist found in database");
            _cache.Set(CacheKey, exist, TimeSpan.FromMinutes(5));
            return exist;
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error while getting exist");
            throw;
        }
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            _logger.LogInformation("Changes saved");
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error while saving changes");
            throw;
        }
    }
}
