using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs;

public class CreateProductDto
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
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    
    [Required]
    [Range(0, 1000000)]
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "\'FileName\' is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "\'filename\' must be between {2} to {1} characters.")]
    [JsonPropertyName("filename")]
    public string FileName { get; set; }

    [Required(ErrorMessage = "\'category-ids\' is required.")]
    [JsonPropertyName("category-ids")] 
    public ICollection<int> CategoryIds { get; set; }
}