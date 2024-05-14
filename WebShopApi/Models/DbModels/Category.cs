using System.ComponentModel.DataAnnotations;

namespace WebShop.Models.DbModels;

public class Category
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(100)]
    public string Description { get; set; }

    // Navigational Properties
    public ICollection<ProductCategory> ProductCategories { get; set; }
}