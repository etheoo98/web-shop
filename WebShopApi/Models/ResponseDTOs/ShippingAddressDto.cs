using System.Text.Json.Serialization;

namespace WebShop.Models.ResponseDTOs
{
    public class ShippingAddressDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("first-name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last-name")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

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
    }
}
