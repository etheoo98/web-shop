namespace WebShopClient.Models.ResponseVMs
{
    public class Shipment
    {
        public int Id { get; set; }
        public DateTime ShippedDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
