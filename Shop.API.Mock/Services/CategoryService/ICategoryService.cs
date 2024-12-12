using System;
using Shop.API.Mock.Models.Category;

namespace Shop.API.Mock.Services;

public interface ICategoryService
{
    Category? GetCategoryById(int id);
    List<Category> GetAll();
    Category? GetCategoryByName(string name);
}
