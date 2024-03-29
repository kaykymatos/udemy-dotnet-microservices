﻿using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Repository;
using GeekShopping.ProductApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ProductVO product)
        {
            if (product is null)
                return BadRequest();

            ProductVO createProduct = await _repository.Create(product);
            return Ok(createProduct);
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(ProductVO product)
        {
            if (product is null)
                return BadRequest();

            ProductVO createProduct = await _repository.Update(product);
            return Ok(createProduct);
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            IEnumerable<ProductVO> productList = await _repository.FindAll();
            if (productList is null)
                return NoContent();
            return Ok(productList);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> FindById(long id)
        {
            ProductVO product = await _repository.FindById(id);
            if (product.Id <= 0)
                return NotFound();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(long id)
        {
            bool deleteProduct = await _repository.Delete(id);
            if (deleteProduct)
                return Ok();
            return BadRequest();
        }
    }
}
