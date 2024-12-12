using System;
using Shop.API.Mock.Models.Category;
using Shop.API.Mock.Models.Product;

namespace Shop.API.Mock.Data;

public class MockAppContext
{
    public List<Product> Products;
    public List<Category> Categories;

    public MockAppContext()
    {
        Products = [];
        Categories = [];
    }
}