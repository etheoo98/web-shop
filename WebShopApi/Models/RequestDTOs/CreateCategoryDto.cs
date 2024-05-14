using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs;

public class CreateCategoryDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
}