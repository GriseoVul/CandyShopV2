using System;

namespace Shop.API.Models.Category;

public class CategoryMetadata
{
    public int CategoryCount { get; set; }
    public ICollection<string> CategoryNameList { get; set; } = [];
}
