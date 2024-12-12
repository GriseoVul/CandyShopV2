using System;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Shop.API.Mock.Models.Category;

public class Category()
{
    public int Id { get;set; }
    public string Name { get; set; } = String.Empty;
    public string[] Characteristics { get; set; } = [];
}
