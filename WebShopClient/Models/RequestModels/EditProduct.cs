using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShopClient.Models.RequestModels
{
    public class EditProduct
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [JsonPropertyName("name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Product name must be between {2} and {1} characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product description is required.")]
        [JsonPropertyName("description")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Product description must be between {2} and {1} characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [JsonPropertyName("price")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive value.")]        
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [JsonPropertyName("quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        [JsonPropertyName("category-ids")]
        public ICollection<int> CategoryIds { get; set; }
    }
}
