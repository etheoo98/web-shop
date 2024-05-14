namespace WebShopClient.Models.ResponseVMs
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalSum { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsPaid { get; set; }
        public Shipment? Shipment { get; set; }   
        public ICollection<Product> Products { get; set; }
    }
}
