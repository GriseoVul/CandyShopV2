using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.API.Entities;

public class CategoryCharacteristic(string name)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = name;

    [Required]
    public int CategoryId { get; set;}

    [ForeignKey("CategoryId")]
    public Category Category{ get; set; } = null!;
}
