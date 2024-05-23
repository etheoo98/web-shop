using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models.DbModels;

//
// Junction Table for Customer and Order
//
public class CustomerOrder
{
    public int Id { get; set; }

    // Foreign Keys
    [ForeignKey("Customer")]
    public int FkCustomerId { get; set; }

    [ForeignKey("Order")]
    public int FkOrderId { get; set; }
    
    // Navigational Properties
    public Customer Customer { get; set; }
    public Order Order { get; set; }
}
