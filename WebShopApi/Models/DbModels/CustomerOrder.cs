using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models.DbModels;

public class CustomerOrder
{
    public int Id { get; set; }
    
    [ForeignKey("Customer")]
    public int FkCustomerId { get; set; }
    
    [ForeignKey("Order")]
    public int FkOrderId { get; set; }

    public Customer Customer { get; set; }
    public Order Order { get; set; }
}