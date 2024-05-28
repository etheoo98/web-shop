using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;
using WebShopClient.Models.RequestModels;
using WebShopClient.Services;

namespace WebShopClient.Controllers
{
    public class AdminController : Controller
    {
        private readonly CustomerService _customerService;
        private readonly ProductService _productService;
        private readonly DiscountService _discountService;
        private readonly IWebHostEnvironment _hostingEnvironment;



        public AdminController(CustomerService customerService, ProductService productService, DiscountService discountService,IWebHostEnvironment hostingEnvironment)
        {
            _customerService = customerService;
            _productService = productService;
            _discountService = discountService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Dashboard()
        {
            var customers = await _customerService.GetCustomersAsync();
            ViewBag.CustomerCount = customers.Count;

            var products = await _productService.GetProductsAsync();
            ViewBag.ProductsCount = products.Count;

            // Sorterar produkterna efter datum och visar dom tre senaste
            var latestProducts = products.OrderByDescending(p => p.AddDate).Take(3).ToList();
            ViewBag.LatestProducts = latestProducts;           

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
                    try
                    {
                        //var fileName = Path.GetFileNameWithoutExtension(createProduct.ImageFile.FileName);
                        var extension = Path.GetExtension(createProduct.ImageFile.FileName);
                        createProduct.FileName = createProduct.FileName + extension;

                        var filePath = Path.Combine(uploadsFolder, createProduct.FileName);
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
                    return RedirectToAction(nameof(Dashboard));
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
            return View(products);
        }

        // GET: /Admin/EditProduct/5
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productService.GetProductAsync(id);
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
                var result = await _productService.UpdateProductAsync(editProduct);
                if (result)
                {
                    return RedirectToAction(nameof(ManageProducts));
                }
            }
            return View(editProduct);
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
                    return RedirectToAction(nameof(Dashboard));
                }
            }
            var products = await _productService.GetProductsAsync();
            ViewBag.Products = new SelectList(products, "Id", "Name");
            return View(createDiscount);
        }
    }
}

