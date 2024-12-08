using System;

namespace Shop.API.Mock.Models.Product;

public struct FoodValue {
    public float Protein { get; set; } = 0;
    public float Fat { get; set; } = 0;
    public float Carbohydrate { get; set; } = 0;
    public float Calories { get; set; } = 0;
    public FoodValue(){}
}