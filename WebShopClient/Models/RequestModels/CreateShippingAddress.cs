using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace WebShopClient.Models.RequestModels
{
    public class CreateShippingAddress
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First name must be between {2} and {1} characters.")]
        [JsonPropertyName("first-name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name must be between {2} and {1} characters.")]
        [JsonPropertyName("last-name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Email must be between {2} and {1} characters.")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Phone number must be between {2} and {1} characters.")]
        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Street must be between {2} and {1} characters.")]
        [JsonPropertyName("street")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Postal code ist required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Postal code must be between {2} and {1} characters.")]
        [DisplayName("Postal code")]
        [JsonPropertyName("postal-code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City must be between {2} and {1} characters.")]
        [JsonPropertyName("city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Country must be between {2} and {1} characters.")]
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("customer-id")]
        public int CustomerId { get; set; }
    }
}
