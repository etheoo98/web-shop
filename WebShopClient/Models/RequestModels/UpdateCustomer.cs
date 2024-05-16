using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShopClient.Models.RequestModels
{
    public class UpdateCustomer
    {
        [Required(ErrorMessage = "Email is required.")]
        [JsonPropertyName("email")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be between {2} and {1} characters.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [JsonPropertyName("first-name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First name must be between {2} to {1} characters.")]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [JsonPropertyName("last-name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name must be between {2} to {1} characters. ")]
        [DisplayName("Last name")]
        public string LastName { get; set; }
    }
}
