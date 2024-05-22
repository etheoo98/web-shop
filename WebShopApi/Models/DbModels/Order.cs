using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Models.ResponseDTOs;

namespace WebShop.Models.DbModels;

public class Order
{
    public int Id { get; set; }   
    public decimal TotalSum { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public bool IsPaid { get; set; }

    // Foreign Keys   
    [ForeignKey("Shipment")]
    public int? FkShipmentId { get; set; } 

    // Navigational Properties
    public Customer? Customer { get; set; }
    public Shipment? ShipmentDetails { get; set; }

    [ForeignKey("FkOrderId")]
    public ICollection<CustomerOrder> CustomerOrders { get; set; }
    public ICollection<OrderProducts>? OrderProducts { get; set; }      
}