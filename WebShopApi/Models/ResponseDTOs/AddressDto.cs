using System.Text.Json.Serialization;

namespace WebShop.Models.ResponseDTOs;

public class AddressDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("street")]
    public string Street { get; set; }

    [JsonPropertyName("postal-code")]
    public string PostalCode { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }          

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("customer-id")]
    public int CustomerId { get; set; }
}