using System;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Shop.API.Mock.Models.Category;

public class Category
(
    string name,
    string[] characteristics
)
{
    public int Id { get;set; }
    public string Name { get; set; } = name;
    public string[] Characteristics { get; set; } = characteristics;
}
