using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;
using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;
using WebShopClient.Services;

namespace WebShopClient.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _api;

        public CategoryController(CategoryService api)
        {
            _api = api;
        }

        // GET: 
        public async Task<IActionResult> Index()
        {
            var categories = await _api.GetCategoriesAsync();
            if (categories == null || !categories.Any())
            {
                return View(new List<Category>());
            }

            return View(categories);
        }
        // GET: /Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategory createCategory)
        {
            if (ModelState.IsValid)
            {
                var result = await _api.CreateCategoryAsync(createCategory);
                if (result)
                {
                    return RedirectToAction(nameof(Create));
                }
            }
            return View(createCategory);
        }

    }
}

