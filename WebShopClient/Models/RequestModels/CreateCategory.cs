using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShopClient.Models.RequestModels
{
    public class CreateCategory
    {
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Category name must be between {2} and {1} characters.")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category description is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Category description must be between {2} and {1} characters.")]
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
