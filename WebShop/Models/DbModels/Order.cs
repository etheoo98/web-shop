namespace WebShop.Models.DbModels;

public class Order
{
    public int Id { get; set; }
    
    public decimal TotalSum { get; set; }
    
    public DateTime OrderDate { get; set; }
    
    public bool IsPaid { get; set; }

    public ICollection<OrderProducts> OrderProducts { get; set; }
}