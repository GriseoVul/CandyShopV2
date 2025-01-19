using System;

namespace Shop.API.Models.Promotion
{
    public class PromotionDto
    {
        public int ID { get; set; }
        public ICollection<int> ProductIDs { get; set; } = [];

        public string Name { get; set; } = String.Empty;

        public string PromotionTag { get; set; } = String.Empty;

        public ICollection<BannerDto> Banners { get; set; } = [];

        public DateTime StartAt { get; set; } = DateTime.MinValue;
    
        public DateTime EndAt { get; set; } = DateTime.MinValue;

        public bool IsActive { get; set; } = false;

        public string Description { get; set; } = String.Empty;

        public int PromotionRating { get; set; } = 0;
    }
}
