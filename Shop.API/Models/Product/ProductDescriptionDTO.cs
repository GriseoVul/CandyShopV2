using System;

namespace Shop.API.Models.Product
{
    public class ProductDescriptionDTO
    {
        public ProductCharacteristicDTO[] Characterisitcs {get; set;} = [];
        public string About { get; set; } = "";
        public FoodValueDTO? FoodValue { get; set;} = null;
    }
}
