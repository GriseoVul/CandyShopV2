using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.API.Entities.Promotion
{
    public class Banner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = " Provide a Name! ")]
        [MaxLength(50, ErrorMessage = " Name is too long! ")]
        public string Name { get; set; } = String.Empty;

        [MaxLength(200)]
        public string UrlPromo { get; set; } = String.Empty;

        [MaxLength(200)]
        public string ImageBlockOutUrl { get; set; } = String.Empty;

        [MaxLength(200)]
        public string ImageModalUrl { get; set; } = String.Empty;
    
    }
}
