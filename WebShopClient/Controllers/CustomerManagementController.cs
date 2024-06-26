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

          // GET: CustomerManagement/Details
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

        // GET: Customer/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            var updateCustomerDto = new UpdateCustomer
            {
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };

            return View(updateCustomerDto);
        }

        // POST: Customer/Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateCustomer updateCustomer)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerService.UpdateCustomerAsync(id, updateCustomer);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(updateCustomer);
        }

    }
}
