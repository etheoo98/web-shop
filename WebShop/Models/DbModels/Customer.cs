using System.ComponentModel.DataAnnotations;

namespace WebShop.Models.DbModels;

public class Customer
{
    public int Id { get; set; }
    
    [StringLength(100, MinimumLength = 5)]
    public string Email { get; set; }
    
    [StringLength(100, MinimumLength = 5)]
    public string Password { get; set; }
    
    [StringLength(100, MinimumLength = 2)]
    public string FirstName { get; set; }
    
    [StringLength(100, MinimumLength = 2)]
    public string LastName { get; set; }
    
    public ICollection<CustomerOrder> CustomerOrders { get; set; }
}