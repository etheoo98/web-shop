using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs;

public class CreateDiscountDto
{
    [Required(ErrorMessage = "product-id is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "\'product-id\' must a positive 32 bit integer.")]
    [JsonPropertyName("product-id")]
    public int ProductId { get; set; }
    
    [Required(ErrorMessage = "percent is required.")]
    [Range(1, 99, ErrorMessage = "\'percent\' must be between {1} to {2}")]
    [JsonPropertyName("percent")]
    public int Percent { get; set; }
    
    [Required(ErrorMessage = "\'start-date\' is required.")]
    [JsonPropertyName("start-date")]
    public DateTime StartDate { get; set; }
    
    [Required(ErrorMessage = "\'end-date\' is required.")]
    [JsonPropertyName("end-date")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "\'discounted-price\' is required.")]
    [JsonPropertyName("discounted-price")]
    public decimal DiscountedPrice { get; set; }
}