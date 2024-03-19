using FashionShopBackend.Interface;
using Microsoft.AspNetCore.Mvc;
using FashionShopBackend.Model;
using FashionShopBackend.Dto;
using Microsoft.AspNetCore.Authorization;

namespace FashionShopBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetAllProduct() {
            var products = _productRepository.getAllProduct();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(products);
        }
        [HttpGet("{product_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult getProductByID(int product_id)
        {
            var product = _productRepository.GetProductById(product_id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(product);
        }
        [HttpPost]
        [Authorize]
        public IActionResult addProduct(ProductDto product)
        {
            _productRepository.addProduct(product);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(product);
        }
        [HttpPut]
        [Authorize]
        public IActionResult editProduct(ProductDto product)
        {
            _productRepository.editProduct(product);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(product);
        }
        [HttpDelete]
        [Authorize]
        public IActionResult deleteProduct(int id)
        {
            _productRepository.deleteProduct(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok();
        }
    }
}
