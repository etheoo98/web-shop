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
        public async Task<IActionResult> AddToCart(int productId, int quantity, string returnUrl)
        {
            await _shoppingCartService.AddToCartAsync(productId, quantity);
            TempData["AddToCartMessage"] = "added to your cart.";

            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Products");
            }
            
            return Redirect(returnUrl);
        }

        public IActionResult GetCartItems()
        {
            var cartItems = _shoppingCartService.GetCartItems();            
            return View(cartItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateItemQuantity(int productId, int quantity)
        {
            _shoppingCartService.UpdateItemQuantity(productId, quantity);            
            return RedirectToAction("GetCartItems");
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
