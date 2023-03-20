using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{    
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        public async Task<IActionResult> ProductIndex()
        {
            var products = await productService.FindAllProducts("");
            return View(products);
        }
        [Authorize]
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductCreate(ProductViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (ModelState.IsValid)
            {
                var response = await productService.CreateProducts(model,token);

                if (response != null) return RedirectToAction(nameof(ProductIndex));
            }
            
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> ProductUpdate(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var model = await productService.FindByIdProducts(id, token);
            if (model != null) return View(model);
            return NotFound();
        }
        
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductUpdate(ProductViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (ModelState.IsValid)
            {
                var response = await productService.UpdateProducts(model, token);

                if (response != null) return RedirectToAction(nameof(ProductIndex));
            }

            return View(model);
        }
        public async Task<IActionResult> ProductDelete(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var model = await productService.FindByIdProducts(id, token);
            if (model != null) return View(model);
            return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await productService.DeleteByIdProducts(model.Id, token);

                if (response) return RedirectToAction(nameof(ProductIndex));

            return View(model);
        }

    }
}
