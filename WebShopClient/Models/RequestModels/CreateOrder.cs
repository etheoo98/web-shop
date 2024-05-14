using System.ComponentModel.DataAnnotations;
using WebShopClient.Models.ResponseVMs;

namespace WebShopClient.Models.RequestModels
{
    public class CreateOrder
    {        
        public decimal TotalSum { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public bool IsPaid { get; set; }    

        public int CustomerId { get; set; }

        public Shipment? Shipment { get; set; }

        public ICollection<int> ProductIds { get; set; }
    }
}
