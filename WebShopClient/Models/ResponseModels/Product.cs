namespace WebShopClient.Models.ResponseVMs
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime AddDate { get; set; } = DateTime.UtcNow;
        public bool IsDiscontinued { get; set; }
        public Discount Discount { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
