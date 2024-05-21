using System.Text.Json.Serialization;

namespace WebShop.Models.ResponseDTOs;

public class CustomerDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    [JsonPropertyName("first-name")]
    public string FirstName { get; set; }
    
    [JsonPropertyName("last-name")]
    public string LastName { get; set; }

    [JsonPropertyName("address")]
    public AddressDto AddressDto { get; set; }
    
    //[JsonPropertyName("orders")]
    [JsonIgnore]
    public ICollection<OrderDto> OrderDtos { get; set; } = [];
}