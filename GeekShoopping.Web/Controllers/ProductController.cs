using GeekShoopping.Web.Models;
using GeekShoopping.Web.Services.IServices;
using GeekShoopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult> ProductIndex()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var products = await _service.FindAllProducts(token);
            return View(products);
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ProductCreate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");

                var postProduct = await _service.CreateProduct(model, token);
                if (postProduct != null)
                    return RedirectToAction(nameof(ProductIndex));
            }

            return View(model);
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult> ProductUpdate(long id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var product = await _service.FindProductById(id, token);
            if (product != null)
                return View(product);
            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ProductUpdate(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");

                var updateProduct = await _service.UpdateProduct(product, token);
                if (product != null)
                    return RedirectToAction(nameof(ProductIndex));
            }
            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var model = await _service.FindProductById(id, token);
            if (model != null)
                return View(model);
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var response = await _service.DeleteProductById(model.Id,token);
            if (response)
                return RedirectToAction(nameof(ProductIndex));
            return View(model);
        }
    }
}
