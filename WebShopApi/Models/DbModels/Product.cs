using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models.DbModels;

public class Product
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(100)]
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime AddDate { get; set; } = DateTime.Now;
    public bool IsDiscontinued { get; set; }
    
    // Foreign Keys
    [ForeignKey("Discount")]
    public int? FkDiscountId { get; set; }
    
    // Navigational Properties
    public Discount? Discount { get; set; }
    public ICollection<OrderProducts>? OrderProducts { get; set; }
    public ICollection<ProductCategory>? ProductCategories { get; set; }
}