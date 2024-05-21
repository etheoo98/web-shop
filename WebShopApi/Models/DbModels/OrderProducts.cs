using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models.DbModels;

//
// Junction Table for Order and Products
//
public class OrderProducts
{
    public int Id { get; set; }    

    // Foreign Keys
    [ForeignKey("Order")]
    public int FkOrderId { get; set; }
    
    [ForeignKey("Product")]
    public int FkProductId { get; set; }

    public int Quantity { get; set; }

    // Navigational Properties
    public Order Order { get; set; }
    public Product Product { get; set; }
}