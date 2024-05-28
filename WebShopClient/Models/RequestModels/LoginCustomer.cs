using System.Text.Json.Serialization;

namespace WebShopClient.Models.RequestModels;

public class LoginCustomer
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
}