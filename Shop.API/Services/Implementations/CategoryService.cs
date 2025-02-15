using System;

namespace Shop.API.Services.Implementations;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Shop.API.Data;
using Shop.API.Entities.Category;
using Shop.API.Services.Interfaces;
public class CategoryService 
(
    ApplicationDBContext context,
    ILogger<ProductService> logger,
    IMemoryCache cache
): ICategoryService
{
    private readonly ApplicationDBContext _context = context;
    private readonly IMemoryCache _cache = cache;
    private readonly ILogger<ProductService> _logger = logger;

    public async Task<Category> CreateAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Category> UpdateAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Category> DeleteAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
         if(_cache.TryGetValue(id, out Category? category))
        {
            _logger.LogInformation("Category {id} found in cache", id);
            return category;
        }

        try
        {
            category = await _context.Categories.FindAsync(id);
            if(category != null)
            {
                _logger.LogInformation("Category {id} found in database", id);
                _cache.Set(id, category, TimeSpan.FromMinutes(5));
                return category;
            }
            _logger.LogWarning("Category {id} not found", id);
            throw new KeyNotFoundException($"Category {id} not found");
        }
        catch( Exception e)
        {
            _logger.LogError(e, "Error while getting product {id}", id);
            throw ;
        }
    }
    public async Task<Category> GetByNameAsync(string name)
    {

         if(_cache.TryGetValue(name, out Category? category))
        {
            _logger.LogInformation("Category {id} found in cache", name);
            return category;
        }

        try
        {
            category = await _context.Categories.FindAsync(name);
            if(category != null)
            {
                _logger.LogInformation("Category {name} found in database", name);
                _cache.Set(name, category, TimeSpan.FromMinutes(5));
                return category;
            }
            _logger.LogWarning("Category {name} not found", name);
            throw new KeyNotFoundException($"Category {name} not found");
        }
        catch( Exception e)
        {
            _logger.LogError(e, "Error while getting product {name}", name);
            throw ;
        }
    }

    public async Task<IQueryable<Category>> GetAllAsync()
    {
         if(_cache.TryGetValue("AllCategories", out IQueryable<Category>? categories))
        {
            _logger.LogInformation("AllCategories found in cache");
            return categories;
        }

        try
        {
            categories = _context.Categories;
            _logger.LogInformation("AllCategories found in database");
            _cache.Set("AllCategories", categories, TimeSpan.FromMinutes(5));
            return categories;
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error while getting AllCategories");
            throw;
        }
    }

    public async Task<IQueryable<Category>> GetByConditionAsync(Expression<Func<Category, bool>> expression)
    {
        string CacheKey = expression.Print();

        if(_cache.TryGetValue(CacheKey, out IQueryable<Category>? categories))
        {
            _logger.LogInformation("Categories found in cache");
            return categories;
        }

        try
        {
            categories = _context.Categories.Where(expression);
            _logger.LogInformation("Categories found in database");
            _cache.Set(CacheKey, categories, TimeSpan.FromMinutes(5));
            return categories;
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error while getting categories");
            throw;
        }
    }

    public async Task<int> CountAsync()
    {
        if(_cache.TryGetValue("CategoryCount", out int count))
        {
            _logger.LogInformation("CategoryCount count found in cache");
            return count;
        }

        try
        {
            count = await _context.Categories.CountAsync();
            _logger.LogInformation("CategoryCount found in database");
            _cache.Set("CategoryCount", count, TimeSpan.FromMinutes(5));
            return count;
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error while getting CategoryCount");
            throw;
        }
    }

    public async Task<bool> ExistAsync(Expression<Func<Category, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while saving changes");
            throw;
        }        
    }
}
