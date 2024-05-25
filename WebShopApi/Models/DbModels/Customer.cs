using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    
    [MaxLength(100)]
    public string Password { get; set; }
    
    [MaxLength(100)]
    public string Role { get; set;}
    
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [MaxLength(100)]
    public string LastName { get; set; }
    
    // Navigational Properties
    public Address? Address { get; set; }
    public ICollection<CustomerOrder> CustomerOrders { get; set; }     
}