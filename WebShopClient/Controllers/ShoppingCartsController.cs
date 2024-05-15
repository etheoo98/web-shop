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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int productId, string productName, decimal price, int quantity)
        {
            _shoppingCartService.AddToCart(productId, productName, price, quantity);

            return RedirectToAction("Index", "Products");
        }

        public IActionResult GetCartItems()
        {
            var cartItems = _shoppingCartService.GetCartItems();

            return View(cartItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveCartItem(int productId)
        {
            _shoppingCartService.RemoveCartItem(productId);

            return RedirectToAction("GetCartItems");
        }
    }
}
