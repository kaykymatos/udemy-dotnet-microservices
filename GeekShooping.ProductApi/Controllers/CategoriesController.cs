using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GeekShooping.ProductApi.Model;
using GeekShooping.ProductApi.Repository;
using GeekShooping.ProductApi.Data.ValueObjects;

namespace GeekShooping.ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryVO category)
        {
            if (category is null)
                return BadRequest();

            var createCategory =await  _repository.Create(category);
            return Ok(createCategory);
        }


        [HttpPut]
        public async Task<IActionResult> Update(CategoryVO category)
        {
            if (category is null)
                return BadRequest();

            var createCategory = await _repository.Update(category);
            return Ok(createCategory);
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var categoryList = await _repository.FindAll();
            if (categoryList is null)
                return NoContent();
            return Ok(categoryList);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(long id)
        {
            var category = await _repository.FindById(id);
            if (category.Id >= 0)
                return NotFound();
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleteCategory = await _repository.Delete(id);
            if (deleteCategory)
                return Ok();
            return BadRequest();
        }
    }
}
