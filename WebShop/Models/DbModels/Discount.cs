namespace WebShop.Models.DbModels;

public class Discount
{
    public int Id { get; set; }
    public int Percent { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
}