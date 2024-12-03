using System;

namespace Shop.API.Mock.Models.Product;

class FoodValue (
    float protein,
    float fat,
    float carbohydrate,
    float calories
){
    public float Protein { get; } = protein;
    public float Fat { get; }= fat;
    public float Carbohydrate { get; }= carbohydrate;
    public float Calories { get; } = calories;
}