using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs;

public class LoginCustomerDto
{
    [Required(ErrorMessage = "\'email\' is required.")]
    [JsonPropertyName("email")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "\'email\' must be between {2} and {1} characters.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "\'password\' is required.")]
    [JsonPropertyName("password")]
    public string Password { get; set; }
}