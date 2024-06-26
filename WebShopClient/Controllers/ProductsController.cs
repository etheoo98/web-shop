﻿using Microsoft.AspNetCore.Mvc;
using WebShopClient.Models.ResponseModels;
using WebShopClient.Services;

namespace WebShopClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _api;

        public ProductsController(ProductService api)
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

        // GET: Product
        public async Task<IActionResult> Details(int id)
        {
            var product = await _api.GetProductAsync(id);

            if (product == null) throw new NotImplementedException();

            return View(product);
        }

        // GET: Products/Search
        public async Task<IActionResult> Search(string searchString)
        {
            var products = await _api.GetProductsAsync();
            var filteredProducts = products
                .Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .Select(p => new { p.Id, p.Name, p.Price, p.FileName })
                .ToList();
            return Json(filteredProducts);
        }

        // GET: products by category
        public async Task<IActionResult> Category(string[] category)
        {
            if (category == null || category.Length == 0)
            {
                return BadRequest("Category cannot be null or empty.");
            }

            var products = await _api.GetProductsByCategoryAsync(category.ToList());

            if (products == null || products.Count == 0)
            {
                return NotFound("No products found for the specified category.");
            }

            return View(products);
        }
    }
}
