using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.API.Entities.Category
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = String.Empty;

        [StringLength(2_000)]
        public string Description { get; set; } = String.Empty;
    
        public ICollection<CategoryCharacteristic> Characteristics { get; set; } = [];

        public ICollection<Product.Product> Products { get; set; } = [];
    }
}
