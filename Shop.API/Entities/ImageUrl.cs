using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.API.Entities;

public class ImageUrl (string url)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Url { get; set; } = url;

    [ForeignKey("ProductId")]
    public Product Product { get; set; } = null!;
    public int ProductId { get; set; }
}
