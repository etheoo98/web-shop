using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopClient.Models.RequestModels;
using WebShopClient.Services;

namespace WebShopClient.Controllers;

public class CustomersController(CustomerService service) : Controller
{
    public IActionResult Login()
    {
        return View();
    }
    
    [Authorize]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("JwtToken");
        return RedirectToAction("Login");
    }
        
    [Authorize]
    public async Task<IActionResult> Orders()
    {
        var currentUser = await service.GetCurrentUser();
        if (currentUser == null) RedirectToAction("Login");
        return View(currentUser);
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginCustomer model)
    {
        var success = await service.AttemptLogin(model);
        if (!success) return View();
        return RedirectToAction("Index", "Home");
    }
}