using System;

namespace Shop.API.Mock.Data;

public class ProductRequest
{
    public int Page { get; set; }
    public int PageLimit { get; set; }
    public ProductRequestOptions Options {get; set;}

}