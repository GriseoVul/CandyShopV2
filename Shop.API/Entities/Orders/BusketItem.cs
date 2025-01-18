using System;

namespace Shop.API.Entities;

public class BusketItem
{
    public int ID { get; set; } = 0;
    public int ProductId { get; set; } = 0;
    public float TotalPrice { get; set; } = 0.0f;
}
