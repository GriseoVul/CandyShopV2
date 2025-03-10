using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.RateLimiting;

namespace Shop.API.Entities.Product
{
    public class Product(string name)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Provide a Name!")]
        [MaxLength(50, ErrorMessage = "Name is too long!")]
        public string Name { get; set; } = name;

        [MaxLength(10)]
        public string Units { get; set; } = String.Empty;

        [Range(0, int.MaxValue)]
        public int Count { get; set; } = 0;
    
        [Range(0, float.MaxValue)]
        public float Price  { get; set; } = 0.0f;

        [Range(0, 100)]
        public int Discount { get; set; } = 0;

        [Range(0, 100)]
        public int Rating { get; set; } = 0;

        public ICollection<ImageUrl> ImageUrls { get; set; } = [];

        [MaxLength(50)]
        public string PromoTag { get; set; } = string.Empty;

        [Required]
        public string CategoryName { get; set; } = String.Empty;
        
        [ForeignKey("CategoryName")]
        public Category.Category Category { get; set; } = null!;

        [ForeignKey("DescriptionId")]
        public ProductDescription Description { get; set; } = null!;

        [Required]
        public string SKU { get; set; } = String.Empty;

        [NotMapped]
        public float TotalPrice => Price - (Price / 100 * Discount);
    
        [NotMapped]
        public bool InStock => Count > 0;
    }
}
