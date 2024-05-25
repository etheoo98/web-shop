using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs;

public class CreateCategoryDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 2)]
    [JsonPropertyName("description")]
    public string Description { get; set; }
}