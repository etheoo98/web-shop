using Microsoft.AspNetCore.Mvc;
using WebShopClient.Models.ResponseModels;
using WebShopClient.Services;

namespace WebShopClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductServices _api;

        public ProductsController(ProductServices api)
        {
            _api = api;
        }

        // GET: Products
		public async Task<IActionResult> Index()
        {
            var products = await _api.GetProductsAsync();
            if (products == null || !products.Any())
            {
                return View(new List<Product>());
            }

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _api.GetProductAsync(id);

            if (product == null) throw new NotImplementedException();

            return View(product);
        }
    }
}
