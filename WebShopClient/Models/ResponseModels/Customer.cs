namespace WebShopClient.Models.ResponseVMs
{
    public class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
