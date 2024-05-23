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
    
    [Authorize("Admin")]
    public IActionResult Test()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginCustomer model)
    {
        var success = await service.AttemptLogin(model);

        if (success)
        {
            return RedirectToAction("Test");
        }

        return View();
    }
}