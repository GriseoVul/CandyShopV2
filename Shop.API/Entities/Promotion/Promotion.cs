using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.API.Entities.Promotion
{
    public class Promotion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ICollection<int> ProductIDs { get; set; } = [];

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = String.Empty;

        public string PromotionTag { get; set; } = String.Empty;

        public ICollection<Banner> Banners { get; set; } = null!;

        [DataType(DataType.DateTime)]
        public DateTime StartAt { get; set; } = DateTime.MinValue;

        [DataType(DataType.DateTime)]
        public DateTime EndAt { get; set; } = DateTime.MinValue;

        public bool IsActive { get; set; } = false; 

        [MaxLength(2_000)]
        public string Description { get; set; } = String.Empty;

        [Range(0, 100)]
        public int PromotionRating { get; set; } = 0;
    }
}
