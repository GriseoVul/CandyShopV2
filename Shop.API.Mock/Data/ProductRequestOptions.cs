using System;

namespace Shop.API.Mock.Data;

public class ProductRequestOptions()
{
    public string? Name { get; set; } = "";
    public string? Category { get; set; } = "";
    public SortOptions Sort { get; set; } = SortOptions.None;
}