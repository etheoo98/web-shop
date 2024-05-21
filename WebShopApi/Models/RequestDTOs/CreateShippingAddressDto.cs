using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs
{
    public class CreateShippingAddressDto
    {
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
