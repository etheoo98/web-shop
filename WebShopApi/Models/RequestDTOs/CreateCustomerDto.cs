using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs;

public class CreateCustomerDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 2)]
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 2)]
    [JsonPropertyName("first-name")]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 2)]
    [JsonPropertyName("last-name")]
    public string LastName { get; set; }
}