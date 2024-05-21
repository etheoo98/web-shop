using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models.DbModels;

public class Customer
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Email { get; set; }
    
    [StringLength(100, MinimumLength = 5)]
    public string Password { get; set; }
    
    [StringLength(100, MinimumLength = 2)]
    public string FirstName { get; set; }
    
    [StringLength(100, MinimumLength = 2)]
    public string LastName { get; set; }
    
    // Navigational Properties
    public Address? Address { get; set; }

    [ForeignKey("FkCustomerId")]
    public ICollection<CustomerOrder> CustomerOrders { get; set; }     
}