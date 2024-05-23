using WebShopClient.Models.ResponseModels;

namespace WebShopClient.ViewModels
{
    public class OrderConfirmationViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalSum { get; set; }
        public ICollection<OrderItemViewModel> OrderItems { get; set; }
        public ShippingAddressViewModel ShippingAddress { get; set; }
    }
}
