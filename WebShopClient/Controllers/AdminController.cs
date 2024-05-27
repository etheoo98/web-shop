﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;
using WebShopClient.Services;

namespace WebShopClient.Controllers;

[Authorize("Admin")]
public class AdminController(
    OrderService orderService,
    CustomerService customerService,
    ProductService productService,
    DiscountService discountService,
    IWebHostEnvironment hostingEnvironment)
    : Controller
{
    public async Task<IActionResult> Dashboard()
    {
        var customers = await customerService.GetCustomersAsync();
        ViewBag.CustomerCount = customers.Count;

        var products = await productService.GetProductsAsync();
        ViewBag.ProductsCount = products.Count;

        var orders = await orderService.GetOrdersAsync();
        ViewBag.OrdersCount = orders.Count;

        // Sorterar produkterna efter datum och visar dom tre senaste
        var latestProducts = products.OrderByDescending(p => p.AddDate).Take(3).ToList();
        ViewBag.LatestProducts = latestProducts;

        return View();
    }

    // GET: /Admin/CreateProduct
    public async Task<IActionResult> CreateProduct()
    {
        var categories = await productService.GetCategoriesAsync();
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
            var uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            if (createProduct.ImageFile.Length > 0)
            {
                var validExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(createProduct.ImageFile.FileName).ToLowerInvariant();

                if (!validExtensions.Contains(extension))
                {
                    ModelState.AddModelError("ImageFile", "Please upload a valid image file (jpg, jpeg, png).");
                    var categories1 = await productService.GetCategoriesAsync();
                    ViewBag.Categories = new SelectList(categories1, "Id", "Name");
                    return View(createProduct);
                }

                try
                {
                    // Apply extension to the specified filename
                    var fileName = createProduct.FileName + extension;
                    createProduct.FileName = fileName;
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await createProduct.ImageFile.CopyToAsync(stream);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while uploading the file.");
                    var categories1 = await productService.GetCategoriesAsync();
                    ViewBag.Categories = new SelectList(categories1, "Id", "Name");
                    return View(createProduct);
                }
            }

            var result = await productService.CreateProductAsync(createProduct);
            if (result) return RedirectToAction(nameof(ManageProducts));
        }

        var categories = await productService.GetCategoriesAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        return View(createProduct);
    }

    // GET: /Admin/ManageProducts
    public async Task<IActionResult> ManageProducts()
    {
        var products = await productService.GetProductsAsync();
        return View(products);
    }

    // GET: /Admin/EditProduct/5
    public async Task<IActionResult> EditProduct(int id)
    {
        var product = await productService.GetProductAsync(id);
        if (product == null) return NotFound();
        var categories = await productService.GetCategoriesAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");

        return View(new EditProduct
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity,
            CategoryIds = product.Categories.Select(c => c.Id).ToList()
        });
    }

    // POST: /Admin/EditProduct/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(int id, EditProduct editProduct)
    {
        if (id != editProduct.Id) return BadRequest();
        if (!ModelState.IsValid)
        {
            var categories = await productService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(editProduct);
        }
        var result = await productService.UpdateProductAsync(editProduct);
        if (result) return RedirectToAction(nameof(ManageProducts));
        return View(editProduct);
    }

    // DELETE: 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await productService.DeleteProductAsync(id);
        if (!result) return BadRequest();
        TempData["SuccessMessage"] = "Product deleted successfully.";
        return RedirectToAction(nameof(ManageProducts));
    }

    // GET: /Admin/CreateDiscount
    public async Task<IActionResult> CreateDiscount()
    {
        var products = await productService.GetProductsAsync();
        ViewBag.Products = new SelectList(products, "Id", "Name");
        return View();
    }

    // POST: /Admin/CreateDiscount
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDiscount(CreateDiscount createDiscount)
    {
        if (ModelState.IsValid)
        {
            var result = await discountService.CreateDiscountAsync(createDiscount);
            if (result) return RedirectToAction(nameof(Dashboard));
        }
        var products = await productService.GetProductsAsync();
        ViewBag.Products = new SelectList(products, "Id", "Name");
        return View(createDiscount);
    }

    // GET: Orders
    public async Task<IActionResult> GetOrders()
    {
        var orders = await orderService.GetOrdersAsync();
        if (orders.Count == 0) return View(new List<Order>());
        return View(orders);
    }
}