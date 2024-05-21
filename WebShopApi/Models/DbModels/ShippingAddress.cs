using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models.DbModels
{
    public class ShippingAddress
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Phone { get; set; }
      
        [MaxLength(100)]
        public string Street { get; set; }

        [MaxLength(100)]
        public string PostalCode { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        // Navigational Properties
        public ICollection<Shipment> Shipments { get; set; }
    }
}
