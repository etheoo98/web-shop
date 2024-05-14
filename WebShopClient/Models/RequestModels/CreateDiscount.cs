using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebShopClient.Models.RequestModels
{
    public class CreateDiscount
    {
        [Required(ErrorMessage = "Please select a product for discount.")]
        [DisplayName("Product")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Percent is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Percent must be a positive value.")]
        public int Percent { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DisplayName("End date")]
        public DateTime EndDate { get; set; }            
    }
}
