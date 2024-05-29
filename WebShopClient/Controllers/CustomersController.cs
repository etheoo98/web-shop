using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopClient.Models.RequestModels;
using WebShopClient.Services;

namespace WebShopClient.Controllers;

public class CustomersController(CustomerService service, ShoppingCartService shoppingCartService) : Controller
{
    public IActionResult Login()
    {
        return View();
    }
        
    [HttpPost]
    public async Task<IActionResult> Login(LoginCustomer model)
    {
        var success = await service.AttemptLogin(model);
        if (!success) return View();
        return RedirectToAction("Index", "Home");
    }
    
    [Authorize]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("JwtToken");
        shoppingCartService.EmptyCart();
        return RedirectToAction("Login");
    }
    
    
    // GET: /Create
    public IActionResult Register()
    {
        return View();
    }
    
    // POST: Create Customer
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(CreateCustomer createCustomer)
    {
        if (ModelState.IsValid)
        {
            var result = await service.CreateCustomerAsync(createCustomer);
            if (result)
            {
                return RedirectToAction("Login");
            }
        }
        return View(createCustomer);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Orders()
    {
        var currentUser = await service.GetCurrentUser();
        if (currentUser == null) RedirectToAction("Login");        

        return View(currentUser);
    }
}