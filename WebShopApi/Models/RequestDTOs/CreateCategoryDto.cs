using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "category is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "\'name\' must be between {2} to {1} characters.")]
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "description is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "\'description\' must be between {2} to {1} characters.")]
    [JsonPropertyName("description")]
    public string Description { get; set; }
}