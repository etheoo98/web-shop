using System.ComponentModel.DataAnnotations;

namespace WebShop.Models.DbModels;

public class Category
{
    public int Id { get; set; }
    
    [StringLength(100, MinimumLength = 5)]
    public string Name { get; set; }
    
    [StringLength(100, MinimumLength = 5)]
    public string Description { get; set; }

    public ICollection<ProductCategory> ProductCategories { get; set; }
}