using System;

namespace Shop.API.Entities;

public class Busket
{
    public ICollection<BusketItem> BusketItems{ get; set; } = [];
    public float BusketPrice {
        get {
            return BusketItems.Sum(i => i.TotalPrice);
        }
    }
}
