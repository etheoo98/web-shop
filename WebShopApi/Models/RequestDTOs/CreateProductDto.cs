using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs;

public class CreateProductDto
{
    [Required(ErrorMessage = "\'name\' is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "\'name\' must be between {2} to {1} characters.")]
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "\'description\' is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "\'description\' must be between {2} to {1} characters.")]
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "\'price\' is required.")]
    [Range(0, 1000000)]
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "\'quantity\' is required.")]
    [Range(0, 1000000, ErrorMessage = "\'quantity\' must be between {1} to {2}.")]
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    
    [Required(ErrorMessage = "\'category-ids\' is required.")]
    [JsonPropertyName("category-ids")] 
    public ICollection<int> CategoryIds { get; set; }
}