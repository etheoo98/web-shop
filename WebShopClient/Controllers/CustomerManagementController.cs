﻿using Microsoft.AspNetCore.Mvc;
using WebShopClient.Services;
using WebShopClient.Models.RequestModels;

namespace WebShopClient.Controllers
{
    public class CustomerManagementController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomerManagementController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetCustomersAsync();
            return View(customers);
        }

        // GET: CustomerManagement/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
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
                var result = await _customerService.CreateCustomerAsync(createCustomer);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(createCustomer);
        }
    }
}
