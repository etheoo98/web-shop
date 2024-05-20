using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebShopClient.Models.RequestModels;
using WebShopClient.Services;

namespace WebShopClient.Controllers
{
    public class AdminController : Controller
    {
        private readonly CustomerService _customerService;
        private readonly ProductServices _productServices;

        public AdminController(CustomerService customerService, ProductServices productServices)
        {
            _customerService = customerService;
            _productServices = productServices;
        }

        public async Task<IActionResult> Dashboard()
        {
            var customers = await _customerService.GetCustomersAsync();
            ViewBag.CustomerCount = customers.Count;

            var products = await _productServices.GetProductsAsync();
            ViewBag.ProductsCount = products.Count;

            // Sorterar produkterna efter datum och visar dom tre senaste
            var latestProducts = products.OrderByDescending(p => p.AddDate).Take(3).ToList();
            ViewBag.LatestProducts = latestProducts;           

            return View();
        }

        // GET: /Admin/CreateProduct
        public async Task<IActionResult> CreateProduct()
        {
            var categories = await _productServices.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        // POST: /Admin/CreateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProduct createProduct)
        {
            if (ModelState.IsValid)
            {
                var result = await _productServices.CreateProductAsync(createProduct);
                if (result)
                {
                    return RedirectToAction(nameof(Dashboard));
                }
            }
            var categories = await _productServices.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(createProduct);
        }

        // GET: /Admin/ManageProducts
        public async Task<IActionResult> ManageProducts()
        {
            var products = await _productServices.GetProductsAsync();
            return View(products);
        }

        // GET: /Admin/EditProduct/5
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productServices.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(new EditProduct
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity
            });
        }

        // POST: /Admin/EditProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, EditProduct editProduct)
        {
            if (id != editProduct.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _productServices.UpdateProductAsync(editProduct);
                if (result)
                {
                    return RedirectToAction(nameof(ManageProducts));
                }
            }
            return View(editProduct);
        }
    }
}

