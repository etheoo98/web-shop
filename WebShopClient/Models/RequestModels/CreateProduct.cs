﻿using System.ComponentModel.DataAnnotations;
using WebShopClient.Models.ResponseVMs;

namespace WebShopClient.Models.RequestModels
{
    public class CreateProduct
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Product name must be between {2} and {1] characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product description is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Product description must be between {2} and {1] characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity name is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "At least one category must be selected.")]
        public ICollection<int> CategoryIds { get; set; }
    }
}