using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Models.Category;
using Shop.API.Services.Interfaces;

namespace Shop.API.Controllers
{
    //TODO add mapping
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(
        ICategoryService categoryService,
        ILogger<CategoriesController> logger
        ) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;
        private readonly ILogger<CategoriesController> _logger = logger;
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            _logger.LogInformation("Getting all categories");
            var categories = await _categoryService.GetAllAsync();

            if(categories is null) 
            {
                _logger.LogInformation("No categories found!");
                return NotFound("No categories");
            }
                

            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                return Ok(category);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetCategory(string name)
        {
            try
            {
                var category = await _categoryService.GetByNameAsync(name);
                return Ok(category);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
