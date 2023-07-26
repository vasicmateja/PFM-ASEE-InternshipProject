using Microsoft.AspNetCore.Mvc;
using PFM_AseeInternship.Services;

namespace PFM_AseeInternship.Controllers
{
    [Route("v1/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        CategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(CategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync([FromQuery] string parentId)
        {
            
            if (parentId == null) {
                return Ok(await _categoryService.GetCategories(""));
            }

            var result = await _categoryService.GetCategories(parentId);

            return Ok(result);
        }


        [HttpPost("import")]
        public IActionResult importTransactions()
        {
            _categoryService.ImportCategories();
            return Ok();
        }
    }
}
