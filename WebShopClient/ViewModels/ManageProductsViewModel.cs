using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.ViewModels
{
    public class ManageProductsViewModel
    {
        public List<Product> Products { get; set; }
        public EditProduct EditProduct { get; set; }
        public CreateDiscount CreateDiscount { get; set; }
    }
}