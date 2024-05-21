using System.Text.Json.Serialization;
using Microsoft.Build.Framework;

namespace WebShop.Models.RequestDTOs;

public class CreateOrderDto
{
    [Required]
    [JsonPropertyName("customer-id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("total-sum")]
    public decimal TotalSum { get; set; }

    [JsonPropertyName("is-paid")]
    public bool IsPaid { get; set; }

    [Required]
    [JsonPropertyName("order-items")]
    public ICollection<CreateOrderItemDto> OrderItems { get; set; }

    [Required]
    [JsonPropertyName("shipment-details")]
    public CreateShipmentDetailsDto ShipmentDetails { get; set; }
}