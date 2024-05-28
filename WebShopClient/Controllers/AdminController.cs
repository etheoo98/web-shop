using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using Newtonsoft.Json.Linq;
using System;
using Microsoft.Extensions.Hosting.Internal;
using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;
using WebShopClient.ViewModels;
using WebShopClient.Services;

namespace WebShopClient.Controllers
{
    public class AdminController : Controller
    {
        private readonly CustomerService _customerService;
        private readonly ProductService _productService;
        private readonly DiscountService _discountService;
        private readonly OrderService _orderService;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public AdminController(OrderService orderService, CustomerService customerService, ProductService productService, DiscountService discountService, IWebHostEnvironment hostingEnvironment)
        {
            _customerService = customerService;
            _productService = productService;
            _discountService = discountService;
            _orderService = orderService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Dashboard()
        {
            var customers = await _customerService.GetCustomersAsync();
            ViewBag.CustomerCount = customers.Count;

            var products = await _productService.GetProductsAsync();
            ViewBag.ProductsCount = products.Count;

            var orders = await _orderService.GetOrdersAsync();
            ViewBag.OrdersCount = orders.Count;

            // Sorterar produkterna efter datum och visar dom tre senaste
            var latestProducts = products.OrderByDescending(p => p.AddDate).Take(3).ToList();
            ViewBag.LatestProducts = latestProducts;

            //räknar totalRevenue
            decimal totalRevenue = orders.Sum(order => order.TotalSum);
            ViewBag.TotalRevenue = totalRevenue;

            // Räknar top products
            var topProducts = orders
                .SelectMany(order => order.OrderProducts)
                .GroupBy(op => new { op.ProductId, op.ProductName })
                .Select(group => new
                {
                    ProductName = group.Key.ProductName,
                    Quantity = group.Sum(op => op.Quantity)
                })
                .OrderByDescending(p => p.Quantity)
                .Take(4)
                .ToList();

            ViewBag.TopProducts = topProducts;


            return View();
        }

        // GET: /Admin/CreateProduct
        public async Task<IActionResult> CreateProduct()
        {
            var categories = await _productService.GetCategoriesAsync();
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
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (createProduct.ImageFile != null && createProduct.ImageFile.Length > 0)
                {
                    var validExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var extension = Path.GetExtension(createProduct.ImageFile.FileName).ToLowerInvariant();

                    if (!validExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("ImageFile", "Please upload a valid image file (jpg, jpeg, png).");
                        var categories1 = await _productService.GetCategoriesAsync();
                        ViewBag.Categories = new SelectList(categories1, "Id", "Name");
                        return View(createProduct);
                    }

                    try
                    {
                        // Apply extension to the specified filename
                        var fileName = createProduct.FileName + extension;
                        createProduct.FileName = fileName;

                        var filePath = Path.Combine(uploadsFolder, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await createProduct.ImageFile.CopyToAsync(stream);
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "An error occurred while uploading the file.");
                        var categories1 = await _productService.GetCategoriesAsync();
                        ViewBag.Categories = new SelectList(categories1, "Id", "Name");
                        return View(createProduct);
                    }
                }

                var result = await _productService.CreateProductAsync(createProduct);
                if (result)
                {
                    return RedirectToAction(nameof(ManageProducts));
                }
            }

            var categories = await _productService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(createProduct);
        }

        // GET: /Admin/ManageProducts
        public async Task<IActionResult> ManageProducts()
        {
            var products = await _productService.GetProductsAsync();
            var categories = await _productService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var model = new ManageProductsViewModel
            {
                Products = products,
                EditProduct = new EditProduct() 
            };

            return View(model);
        }

        // POST: /Admin/EditProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(EditProduct editProduct)
        {
            if (!ModelState.IsValid)
            {
                var products = await _productService.GetProductsAsync();
                var categories = await _productService.GetCategoriesAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");

                var model = new ManageProductsViewModel
                {
                    Products = products,
                    EditProduct = editProduct
                };

                return View("ManageProducts", model);
            }

            var result = await _productService.UpdateProductAsync(editProduct);
            if (result)
            {
                TempData["SuccessMessage"] = "Product updated successfully!";
                return RedirectToAction(nameof(ManageProducts));
            }

            var productsList = await _productService.GetProductsAsync();
            var categoriesList = await _productService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categoriesList, "Id", "Name");

            var modelError = new ManageProductsViewModel
            {
                Products = productsList,
                EditProduct = editProduct
            };

            return View("ManageProducts", modelError);
        }

        //// POST: /Admin/EditProduct/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditProduct(int id, EditProduct editProduct)
        //{
        //    if (id != editProduct.Id)
        //    {
        //        return BadRequest();
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        var categories = await _productService.GetCategoriesAsync();
        //        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        //        return View(editProduct);
        //    }
        //    var result = await _productService.UpdateProductAsync(editProduct);
        //    if (result)
        //    {
        //        return RedirectToAction(nameof(ManageProducts));
        //    }

        //    return View(editProduct);
        //}

        // DELETE: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return BadRequest();
            }

            TempData["SuccessMessage"] = "Product deleted successfully.";
            return RedirectToAction(nameof(ManageProducts));
        }

        // GET: /Admin/CreateDiscount
        public async Task<IActionResult> CreateDiscount()
        {
            var products = await _productService.GetProductsAsync();
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
                var result = await _discountService.CreateDiscountAsync(createDiscount);
                if (result)
                {
                    TempData["SuccessMessage"] = "Campaign created successfully!";
                    return RedirectToAction(nameof(ManageProducts));
                }
            }
            var products = await _productService.GetProductsAsync();
            ViewBag.Products = new SelectList(products, "Id", "Name");
            return View(createDiscount);
        }

        // GET: Orders
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            if (orders == null || !orders.Any())
            {
                return View(new List<Order>());
            }

            return View(orders);
        }

        // Partial View for Edit Product
        public async Task<IActionResult> EditProductPartial(int id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = await _productService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return PartialView("_EditProductPartial", new EditProduct
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryIds = product.Categories.Select(c => c.Id).ToList()
            });
        }


        // Partial View for Create Discount
        public async Task<IActionResult> CreateDiscountPartial(int id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.ProductName = product.Name;

			return PartialView("_CreateDiscountPartial", new CreateDiscount
            {
                ProductId = product.Id,
                StartDate = DateTime.Now
            });
        }

        // GET: Partial view Product discount
        public async Task<IActionResult> GetProductDiscountPartial(int id)
        {
            var product = await _productService.GetProductAsync(id);
            
            if (product == null)
            {
                return NotFound();
            }

            return PartialView("_GetProductDiscountPartial", product);
        }

    }
}

