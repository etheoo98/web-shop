using System.Text.Json.Serialization;

namespace WebShopClient.Models.ResponseVMs
{
    public class Customer
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
        public Address Address { get; set; }

        [JsonPropertyName("orders")]
        public ICollection<Order>? Orders { get; set; }
    }
}
