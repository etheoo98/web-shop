﻿using System.Text.Json.Serialization;

namespace WebShopClient.Models.ResponseVMs
{
    public class Order
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("customer-id")]
        public int CustomerId { get; set; }

        [JsonPropertyName("total-sum")]
        public decimal TotalSum { get; set; }

        [JsonPropertyName("order-date")]
        public DateTime OrderDate { get; set; }

        [JsonPropertyName("is-paid")]
        public bool IsPaid { get; set; }

        [JsonPropertyName("shipment-details")]
        public Shipment? Shipment { get; set; }

        [JsonPropertyName("products")]
        public ICollection<Product> Products { get; set; }
    }
}
