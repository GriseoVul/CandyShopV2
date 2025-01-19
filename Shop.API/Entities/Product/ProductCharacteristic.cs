using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.API.Entities.Product
{
    public class ProductCharacteristic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;}

        [Required]
        [StringLength(50)]
        public string Name {get; set; } = String.Empty;
    
        [Required]
        [MaxLength(50)]
        public string Value {get; set; } = String.Empty;
    }
}
