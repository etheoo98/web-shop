using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebShopClient.Models.RequestVMs
{
    public class CreateCustomer
    {        
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be between {0} and {1} characters.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Password must be between {0} and {1} characters.")]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }    

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First name must be between {0} to {1} characters.")]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name must be between {0} to {1} characters. ")]
        [DisplayName("Last name")]
        public string LastName { get; set; }        
    }
}
