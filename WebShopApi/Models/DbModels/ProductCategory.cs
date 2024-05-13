using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models.DbModels;

//
// Junction Table for Product and Category
//
public class ProductCategory
{
    public int Id { get; set; }
    
    // Foreign Keys
    [ForeignKey("Product")]
    public int FkProductId { get; set; }
    
    [ForeignKey("Category")]
    public int FkCategoryId { get; set; }
    
    // Navigational Properties
    public Product Product { get; set; }
    public Category Category { get; set; }
}