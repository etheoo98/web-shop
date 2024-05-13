using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models.DbModels;

public class OrderProducts
{
    public int Id { get; set; }
    
    [ForeignKey("Order")]
    public int FkOrderId { get; set; }
    
    [ForeignKey("Product")]
    public int FkProductId { get; set; }
    
    public Order Order { get; set; }
    public Product Product { get; set; }
}