using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models.DbModels;

public class Address
{
    public int Id { get; set; }
    
    [StringLength(100, MinimumLength = 2)]
    public string Country { get; set; }
    
    [StringLength(100, MinimumLength = 2)]
    public string City { get; set; }
    
    [StringLength(100, MinimumLength = 2)]
    public string PostalCode { get; set; }
    
    [StringLength(100, MinimumLength = 2)]
    public string Street { get; set; }
    
    [StringLength(100, MinimumLength = 2)]
    public string Phone { get; set; }
    
    // Foreign Keys
    [ForeignKey("Customer")]
    public int FkCustomerId { get; set; }
    
    // Navigational Properties
    public Customer Customer { get; set; }
}