using FashionShopBackend.Dto;
using FashionShopBackend.Interface;
using FashionShopBackend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionShopBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetAllCategory()
        {
            var categorys = _categoryRepository.getAllCategory();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(categorys);
        }
        [HttpGet("{category_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult getCategoryByID(int category_id)
        {
            var category = _categoryRepository.getCategoryById(category_id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(category);
        }
        [HttpPost]
        [Authorize]
        public IActionResult addCategory(CategoryDto category)
        {
            _categoryRepository.addCategory(category);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(category);
        }
        [HttpPut]
        [Authorize]
        public IActionResult editCategory(CategoryDto category)
        {
            _categoryRepository.editCategory(category);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(category);
        }
        [HttpDelete]
        [Authorize]
        public IActionResult deleteCategory(int id)
        {
            _categoryRepository.deleteCategory(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok();
        }
    }
}
