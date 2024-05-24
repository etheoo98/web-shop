using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs;

public class CreateCustomerDto
{
    [Required(ErrorMessage = "Email is required.")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "\'email\' must be between {2} to {1} characters.")]
    [DataType(DataType.EmailAddress)]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "password is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "\'password\' must be between {2} to {1} characters.")]
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "first-name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "\'first-name\' must be between {2} to {1} characters.")]
    [JsonPropertyName("first-name")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "last-name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "\'last-name\' must be between {2} to {1} characters.")]
    [JsonPropertyName("last-name")]
    public string LastName { get; set; }
}