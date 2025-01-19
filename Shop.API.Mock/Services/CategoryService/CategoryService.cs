using System;
using Microsoft.Extensions.Caching.Memory;
using Shop.API.Mock.Data;
using Shop.API.Mock.Models.Category;

namespace Shop.API.Mock.Services
{
    public class CategoryService
    (
        IMemoryCache cache,
        MockAppContext context
    )
    : ICategoryService
    {
        IMemoryCache _cache = cache;
        MockAppContext _context = context;
        public Category? GetCategoryById(int id)
        {
            _cache.TryGetValue(id, out Category? result);
            if (result == null)
            {
                result = _context.Categories.FirstOrDefault(c => c.Id == id);
                if (result != null)
                    _cache.Set(
                            result.Id, 
                            result, 
                            new MemoryCacheEntryOptions()
                                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            }
            return result;
        }
        public Category? GetCategoryByName(string name)
        {
            _cache.TryGetValue(name, out Category? result);
            if (result == null)
            {
                result = _context.Categories.FirstOrDefault(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                if (result != null)
                    _cache.Set(
                            result.Name, 
                            result, 
                            new MemoryCacheEntryOptions()
                                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            }
            return result;
        }
        public List<Category> GetAll()
        {
            return [.. _context.Categories];
        }
    }
}
