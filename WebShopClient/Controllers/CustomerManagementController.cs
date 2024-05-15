using Microsoft.AspNetCore.Mvc;
using WebShopClient.Services;
using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseVMs;

namespace WebShopClient.Controllers
{
    public class CustomerManagementController : Controller
    {
        private readonly ApiServices _apiServices;

        public CustomerManagementController(ApiServices apiServices)
        {
            _apiServices = apiServices;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = await _apiServices.GetCustomersAsync();
            return View(customers);
        }

        // GET: /Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create Customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomer createCustomer)
        {
            if (ModelState.IsValid)
            {
                var result = await _apiServices.CreateCustomerAsync(createCustomer);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(createCustomer);
        }
    }
}
