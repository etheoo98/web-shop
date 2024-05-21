using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models.DbModels;

public class Discount
{
    public int Id { get; set; }
    public int Percent { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal DiscountedPrice { get; set; } 
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}