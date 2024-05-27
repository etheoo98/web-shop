using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebShopClient.Models;

namespace WebShopClient.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    public IActionResult Index()
    {
        // Redirects to Products for now
        return RedirectToAction("Index", "Products");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}