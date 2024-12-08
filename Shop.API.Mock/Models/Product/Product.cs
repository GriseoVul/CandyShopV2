using System;

namespace Shop.API.Mock.Models.Product;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Units { get; set; }
    public int Count { get; set; }
    public float Price  { get; set; }
    public int Discount { get; set; }
    public int Rating { get; set; }
    public string[] ImageUrls { get; set; }
    public string PromoTag { get; set; }
    public string Category { get; set; }
    public ProductDescription Description { get; set; }
    public string StockKeepingUnit { get; set; }
    public float TotalPrice => Price - (Price / 100 * Discount);
    public bool InStock => Count > 0;

}