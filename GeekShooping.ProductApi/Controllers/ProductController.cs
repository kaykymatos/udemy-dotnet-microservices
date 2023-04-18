using GeekShooping.ProductApi.Data.ValueObjects;
using GeekShooping.ProductApi.Model;
using GeekShooping.ProductApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShooping.ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVO product)
        {
            if (product is null)
                return BadRequest();

            var createProduct = await _repository.Create(product);
            return Ok(createProduct);
        }


        [HttpPut]
        public async Task<IActionResult> Update(ProductVO product)
        {
            if (product is null)
                return BadRequest();

            var createProduct = await _repository.Update(product);
            return Ok(createProduct);
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var productList = await _repository.FindAll();
            if (productList is null)
                return NoContent();
            return Ok(productList);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(long id)
        {
            var product = await _repository.FindById(id);
            if (product.Id>=0)
                return NotFound();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleteProduct = await _repository.Delete(id);
            if (deleteProduct)
                return Ok();
            return BadRequest();
        }
    }
}
