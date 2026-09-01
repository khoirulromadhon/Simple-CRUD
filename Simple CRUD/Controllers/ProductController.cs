using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple_CRUD.Services;
using Simple_CRUD.ViewModels;

namespace Simple_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(VMProduct product)
        {
            var createdProduct = await _productService.Create(product);
            return Ok(createdProduct);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? search,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int cursorId = 0)
        {
            var products = await _productService.GetAll(search, minPrice, maxPrice, cursorId);
            return Ok(products);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var result = await _productService.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
