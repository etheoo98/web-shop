using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebShopClient.Models.RequestModels
{
    public class CreateProduct
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Product name must be between {2} and {1} characters.")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product description is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Product description must be between {2} and {1} characters.")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity name is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "FileName is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Product name must be between {2} and {1} characters.")]
        [JsonPropertyName("filename")]
        public string FileName { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Image file is required.")]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "At least one category must be selected.")]
        [JsonPropertyName("category-ids")]
        public ICollection<int> CategoryIds { get; set; }
    }
}
