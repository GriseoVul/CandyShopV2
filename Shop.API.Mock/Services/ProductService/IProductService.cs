using System;
using Shop.API.Mock.Data;
using Shop.API.Mock.Models.Product;

namespace Shop.API.Mock.Services;

public interface IProductService
{
    Product? GetProductBySKU(string sku);
    public Product? GetProduct(int id);
    public List<Product> GetProductsWithParams(ProductRequest request);
    public int AddProduct(Product product);
}
