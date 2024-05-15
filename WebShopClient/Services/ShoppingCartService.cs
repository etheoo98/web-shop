using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Services
{
    //public interface IShoppingCartService
    //{
    //    IEnumerable<ShoppingCartItem> GetCartItemsAsync();
    //    void AddToCartAsync(int productId, int quantity);
    //    void RemoveFromCartAsync(int productId);
    //}
    public class ShoppingCartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }      

        public IEnumerable<ShoppingCartItem> GetCartItems()
        {
            var session = GetSession();


            var productId = session.GetInt32("productId");                            
        }

        private ISession GetSession()
        {
            return _httpContextAccessor.HttpContext.Session;
        }
    }
}
