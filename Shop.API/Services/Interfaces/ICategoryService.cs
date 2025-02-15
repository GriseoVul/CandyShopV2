using System;
using Shop.API.Entities.Category;

namespace Shop.API.Services.Interfaces;

public interface ICategoryService: IBaseService <Category>
{
    Task<Category> GetByNameAsync(string name);
}
