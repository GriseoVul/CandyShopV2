using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Shop.API.Entities.Product
{
    public class ProductDescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public ICollection<ProductCharacteristic> Characterisitcs {get; set;} = [];

        [StringLength(500)]
        public string About { get; set; } = "";

        [MaybeNull]
        public FoodValue? FoodValue { get; set;} = null;
    }
}
