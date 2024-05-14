using System.Text.Json.Serialization;

namespace WebShop.Models.ResponseDTOs;

public class CategoryDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
}