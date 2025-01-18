using System;

namespace Shop.API.Models;

public class ProductDto
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = String.Empty;
    public string Units { get; set; } = String.Empty;
    public int Count { get; set; } = 0;
    public float Price  { get; set; } = 0.0f;
    public int Discount { get; set; } = 0;
    public int Rating { get; set; } = 0;
    public string[] ImageUrls { get; set; } = [];
    public string PromoTag { get; set; } = String.Empty;
    public string Category { get; set; } = String.Empty;
    public ProductDescriptionDTO Description { get; set; } = null!;
    public string SKU { get; set; } = String.Empty;
    public float TotalPrice => Price - (Price / 100 * Discount);
    public bool InStock => Count > 0;

}
