using System.Text.Json.Serialization;

namespace WebShop.Models.ResponseDTOs;

public class ProductDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("filename")]
    public string FileName { get; set; }

    [JsonPropertyName("add-date")]
    public DateTime AddDate { get; set; } = DateTime.UtcNow;
    
    [JsonPropertyName("is-discontinued")]
    public bool IsDiscontinued { get; set; }
    
    [JsonPropertyName("discount")]
    public DiscountDto DiscountDto { get; set; }

    [JsonPropertyName("categories")]
    public ICollection<CategoryDto> CategoryDtos { get; set; } = [];
}