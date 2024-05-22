using System.Text.Json.Serialization;

namespace WebShopClient.ViewModels
{
    public class ShippingAddressViewModel
    {                        
        public string Phone { get; set; }
        
        public string Street { get; set; }
        
        public string City { get; set; }
        
        public string PostalCode { get; set; }
        
        public string Country { get; set; }
    }
}
