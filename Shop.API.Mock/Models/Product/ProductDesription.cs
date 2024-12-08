using System;

namespace Shop.API.Mock.Models.Product;

public class ProductDescription()
{
    public ProductCharacteristic[] Characterisitcs {get; set;} = [];
    public string About { get; set; } = "";
    public FoodValue? FoodValue { get; set;} = null;
}
