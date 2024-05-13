using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models.DbModels;

public class ProductCategory
{
    public int Id { get; set; }
    
    [ForeignKey("Category")]
    public int FkCategoryId { get; set; }
    
    [ForeignKey("Product")]
    public int FkProductId { get; set; }
    
    public Category Category { get; set; }
    public Product Product { get; set; }
}