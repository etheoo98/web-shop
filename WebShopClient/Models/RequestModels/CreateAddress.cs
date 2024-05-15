using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebShopClient.Models.RequestModels
{
    public class CreateAddress  
    {      
        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Phone number must be between {2} and {1} characters.")]
        public string Phone { get; set; }        

        [Required(ErrorMessage = "Street is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Street must be between {2} and {1} characters.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Postal code ist required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Postal code must be between {2} and {1} characters.")]
        [DisplayName("Postal code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Country must be between {2} and {1} characters.")]
        public string Country { get; set; }
        
        public int CustomerId { get; set; }
    }
}
