using Microsoft.AspNetCore.Mvc;
using WebShopClient.Models.ResponseModels;
using WebShopClient.Services;

namespace WebShopClient.Controllers;

public class ProductsController(ProductService productService) : Controller
{
    // GET: Products
    public async Task<IActionResult> Index()
    {
        var products = await productService.GetProductsAsync();
        if (products.Count == 0) return View(new List<Product>());
        return View(products);
    }

    // GET: Product
    public async Task<IActionResult> Details(int id)
    {
        var product = await productService.GetProductAsync(id);
        if (product == null) throw new NotImplementedException();
        return View(product);
    }

    // GET: Products/Search
    public async Task<IActionResult> Search(string searchString)
    {
        var products = await productService.GetProductsAsync();
        var filteredProducts = products
            .Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            .Select(p => new { p.Id, p.Name, p.Price })
            .ToList();
            
        return Json(filteredProducts);
    }  
}