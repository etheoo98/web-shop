using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models.DbModels;

public class Shipment
{
    public int Id { get; set; }
    public DateTime ShippedDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    
    // Foreign Keys
    [ForeignKey("Order")]
    public int FkOrderId { get; set; }
    
    // Navigational Properties
    public Order Order { get; set; }
}