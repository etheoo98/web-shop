using WebShopClient.Models.ResponseModels;

namespace WebShopClient.ViewModels
{
    public class ShipmentDetailsViewModel
    {
        public DateTime ShippedDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public ShippingAddressViewModel ShippingAddress { get; set; }
    }
}
