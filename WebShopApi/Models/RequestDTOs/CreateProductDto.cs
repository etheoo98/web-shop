using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs;

public class CreateProductDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    
    [JsonPropertyName("category-ids")] 
    public ICollection<int> CategoryIds { get; set; }
}