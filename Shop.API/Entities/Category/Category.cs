using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.API.Entities;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get;set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = String.Empty;
    
    public ICollection<CategoryCharacteristic> Characteristics { get; set; } = [];
}
