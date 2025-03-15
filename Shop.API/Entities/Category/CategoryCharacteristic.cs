using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.API.Entities.Category
{
    public class CategoryCharacteristic
    {
        public CategoryCharacteristic() {}
        public CategoryCharacteristic(string name, int categoryId, Category category)
        {
            Name = name;
            CategoryId = categoryId;
            Category = category;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = String.Empty;

        [Required]
        public int CategoryId { get; set;} 
        
        [ForeignKey("CategoryId")]
        public Category Category{ get; set; }
    }
}
