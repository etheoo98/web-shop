using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShopClient.Models.RequestModels
{
    public class CreateDiscount
    {
        [Required(ErrorMessage = "Please select a product for discount.")]
        [DisplayName("Product")]
        [JsonPropertyName("product-id")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Percent is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Percent must be a positive value.")]
        [JsonPropertyName("percent")]
        public int Percent { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [DisplayName("Start date")]
        [JsonPropertyName("start-date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DisplayName("End date")]
        [JsonPropertyName("end-date")]
        public DateTime EndDate { get; set; }            
    }
}
