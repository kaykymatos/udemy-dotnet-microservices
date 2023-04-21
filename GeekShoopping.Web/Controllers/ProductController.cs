using GeekShoopping.Web.Models;
using GeekShoopping.Web.Services;
using GeekShoopping.Web.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShoopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> ProductIndex()
        {
            var products = await _service.FindAllProducts();
            return View(products);
        }



        [HttpGet]
        public async Task<ActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProductCreate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var postProduct = await _service.CreateProduct(model);
                if (postProduct != null)
                    return RedirectToAction(nameof(ProductIndex));
            }
            
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> ProductUpdate(long id)
        {
            var product = await _service.FindProductById(id);
            if (product != null)
                return View(product);
            return NotFound();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProductUpdate(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var updateProduct = await _service.UpdateProduct(product);
                if (product != null)
                    return RedirectToAction(nameof(ProductIndex));
            }
            return View(product);
        }

        public async Task<IActionResult> ProductDelete(int id)
        {
            var model = await _service.FindProductById(id);
            if (model != null) 
                return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductModel model)
        {
            var response = await _service.DeleteProductById(model.Id);
            if (response) 
                return RedirectToAction(nameof(ProductIndex));
            return View(model);
        }
    }
}
