using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models.DbModels;

public class Address
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Country { get; set; }
    
    [MaxLength(100)]
    public string City { get; set; }
    
    [MaxLength(100)]
    public string PostalCode { get; set; }
    
    [MaxLength(100)]
    public string Street { get; set; }
    
    [MaxLength(100)]
    public string Phone { get; set; }
    
    // Foreign Keys
    [ForeignKey("Customer")]
    public int FkCustomerId { get; set; }
    
    // Navigational Properties
    public Customer Customer { get; set; }
}