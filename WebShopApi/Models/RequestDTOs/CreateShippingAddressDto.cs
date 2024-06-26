﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs
{
    public class CreateShippingAddressDto
    {
        [Required(ErrorMessage = "\'first-name\' is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "\'first-name\' must be between {2} to {1} characters.")]
        [JsonPropertyName("first-name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "\'last-name\' is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "\'last-name\' must be between {2} to {1} characters.")]
        [JsonPropertyName("last-name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "\'email\' is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "\'email\' must be between {2} to {1} characters.")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "\'phone\' is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "\'phone\' must be between {2} to {1} characters.")]
        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "\'street\' is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "\'street\' must be between {2} to {1} characters.")]
        [JsonPropertyName("street")]
        public string Street { get; set; }

        [Required(ErrorMessage = "\'postal-code\' is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "\'postal-code\' must be between {2} to {1} characters.")]
        [JsonPropertyName("postal-code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "\'city\' is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "\'city\' must be between {2} to {1} characters.")]
        [JsonPropertyName("city")]
        public string City { get; set; }

        [Required(ErrorMessage = "\'country\' is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "\'country\' must be between {2} to {1} characters.")]
        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}
