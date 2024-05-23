using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs;

public class UpdateProductDto
{
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "\'name\' must be between {2} to {1} characters.")]
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "\'description\' must be between {2} to {1} characters.")]
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [Required]
    [Range(0, 1000000, ErrorMessage = "\'quantity\' must be between {1} to {2}.")]
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    
    [Required]
    [JsonPropertyName("category-ids")] 
    public ICollection<int> CategoryIds { get; set; }
}