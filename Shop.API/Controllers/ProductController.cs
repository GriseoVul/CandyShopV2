using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Models.Product;
using Shop.API.Services.Interfaces;

namespace Shop.API.Controllers
{

    //TODO add mapping
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(
        IProductService productService,
        IMapper mapper
    ) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllAsync();
            var productDtos = _mapper.Map<ProductDto>( products );
            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {            
            try
            {
                var product = await _productService.GetByIdAsync(id);
                var productDtos = _mapper.Map<ProductDto>( product );
                return Ok(productDtos);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("{SKU:string}")]
        public async Task<IActionResult> GetProduct(string SKU)
        {
            try
            {
                var product = await _productService.GetByConditionAsync(p => p.SKU == SKU);
                var productDtos = _mapper.Map<ProductDto>( product );
                return Ok(productDtos);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}