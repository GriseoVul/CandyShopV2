using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Shop.API.Enum;

namespace Shop.API.Entities;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    
    public OrderStatus Status { get; set; } = OrderStatus.None;
    
    [Required]
    [StringLength(13)]
    public string ClientPhone {get; set; } = String.Empty;

    [Required]
    [StringLength(50)]
    public string ClientName { get; set; } = String.Empty;

    [StringLength(200)]
    public string TrackId { get; set; } = String.Empty;

    [Required]
    [StringLength(1_000)]
    public string MailData { get; set; } = String.Empty;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.MinValue;
}
