using System;

namespace Shop.API.Models.Category
{
    public class CategoryDTO
    {
        public int Id { get;set; } = 0;
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public ICollection<string> Characteristics { get; set; } = [];
    }
}
