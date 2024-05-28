using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShop.Models.RequestDTOs;

public class CreateOrderDto
{
    [Required(ErrorMessage = "\'total-sum\' is required.")]
    [JsonPropertyName("total-sum")]
    public decimal TotalSum { get; set; }

    [Required(ErrorMessage = "\'is-paid\' is required.")]
    [JsonPropertyName("is-paid")]
    public bool IsPaid { get; set; }

    [Required(ErrorMessage = "\'order-items\' is required.")]
    [JsonPropertyName("order-items")]
    public ICollection<CreateOrderItemDto> OrderItems { get; set; }

    [Required(ErrorMessage = "\'shipment-details\' is required.")]
    [JsonPropertyName("shipment-details")]
    public CreateShipmentDetailsDto ShipmentDetails { get; set; }
}