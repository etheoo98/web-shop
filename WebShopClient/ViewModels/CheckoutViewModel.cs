using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.ViewModels
{
    public class CheckoutViewModel
    {
        public Customer Customer { get; set; }
        public List<ShoppingCartItem> CartItems { get; set; }
        public decimal TotalSum { get; set; }
        public ShipmentDetailsViewModel? ShipmentDetails { get; set; }
    }
}
