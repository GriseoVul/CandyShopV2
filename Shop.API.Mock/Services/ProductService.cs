using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Shop.API.Mock.Data;
using Shop.API.Mock.Models.Product;

namespace Shop.API.Mock.Services;

public class ProductService 
(
    IMemoryCache cache,
    MockAppContext context
)
: IProductService
{
    IMemoryCache _cache = cache;
    MockAppContext _context = context;

    public Product? GetProduct(int id)
    {
        _cache.TryGetValue(id, out Product? result);
        if(result == null)
        {
            result = _context.Products.FirstOrDefault(pr => pr.Id == id);
            if(result != null )
            {
                Console.WriteLine("Product with id: {id}, found in DataBase!");
                _cache.Set(
                    result.Id, 
                    result, 
                    new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            }
        }
        else 
        {
            Console.WriteLine("Product with id: {id}, extracted from memCache!");
        }
        
        return result;
    }
    public List<Product> GetProductsWithParams(ProductRequest request)
    {

        var ProdRequest = _context.Products;
        
        if (request.Options.Category != null)
        {
            ProdRequest = [.. (from s in ProdRequest
                        where s.Category.ToLower().Equals(request.Options.Category)
                        orderby s.Rating descending
                        select s)];
        }

        if (request.Options.Name != null)
        {
            ProdRequest = [.. from s in ProdRequest
                          where s.Name.Contains( request.Options.Name, StringComparison.InvariantCultureIgnoreCase )
                          orderby s.Rating descending
                          select s];
        }

        ProdRequest = [.. ProdRequest
                    .Skip((request.Page - 1) * request.PageLimit)
                    .Take(request.PageLimit)
                    .OrderByDescending(pr => pr.Rating)];
        
        switch (request.Options.Sort)
        {
            case SortOptions.PriceAscending:
                ProdRequest = [.. from prod in ProdRequest 
                        orderby prod.Rating descending,prod.Price ascending
                        select prod];
            break;

            case SortOptions.PriceDescending:
                ProdRequest = [.. from prod in ProdRequest 
                        orderby prod.Rating descending, prod.Price descending 
                        select prod];
            break;
        }
        return ProdRequest;
    }
}