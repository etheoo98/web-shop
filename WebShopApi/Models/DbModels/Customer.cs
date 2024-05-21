using System.ComponentModel.DataAnnotations;

namespace WebShop.Models.DbModels;

public enum Role
{
    Admin,
    Customer
}

public class Customer
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Email { get; set; }
    
    [StringLength(100, MinimumLength = 5)]
    public string Password { get; set; }
    
    [MaxLength(100)]
    public string Role { get; set;}
    
    [StringLength(100, MinimumLength = 2)]
    public string FirstName { get; set; }
    
    [StringLength(100, MinimumLength = 2)]
    public string LastName { get; set; }
    
    // Navigational Properties
    public ICollection<CustomerOrder> CustomerOrders { get; set; }
}