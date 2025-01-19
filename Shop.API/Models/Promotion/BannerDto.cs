using System;

namespace Shop.API.Models.Promotion
{
    public class BannerDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = String.Empty;

        public string UrlPromo { get; set; } = String.Empty;

        public string ImageBlockOutUrl { get; set; } = String.Empty;

        public string ImageModalUrl { get; set; } = String.Empty;
    }
}
