using Microsoft.AspNetCore.Mvc;
using WebShopClient.Services;

namespace WebShopClient.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartsController(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        //public async Task<IActionResult> ViewCart(int productId, int quantity)
        //{
        //    var cartItems = _shoppingCartService.GetCartItemsAsync();

        //    return View(cartItems);
        //}            

        public IActionResult Index()
        {
            

                

            return View();
        }
    }
}
