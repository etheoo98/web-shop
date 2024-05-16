using Microsoft.AspNetCore.Mvc;

namespace WebShopClient.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
         //Möjligt att lägga till fiktiv data tills vi kan skicka data som vi vill visa.
            return View();
        }
    }
}
