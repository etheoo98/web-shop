using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs;

public class EditProductDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 2)]
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [Required]
    [Range(0, 1000000)]
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    
    [Required]
    [JsonPropertyName("category-ids")] 
    public ICollection<int> CategoryIds { get; set; }
}