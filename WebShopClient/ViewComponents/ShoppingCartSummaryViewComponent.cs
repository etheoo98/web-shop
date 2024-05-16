using Microsoft.AspNetCore.Mvc;
using WebShopClient.Services;

namespace WebShopClient.ViewComponents
{
    public class ShoppingCartSummaryViewComponent : ViewComponent
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartSummaryViewComponent(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public IViewComponentResult Invoke()
        {
            var cartItems = _shoppingCartService.GetCartItems();
            var productsInCart = cartItems.Sum(item => item.Quantity);
            return View(productsInCart);
        }
    }
}
