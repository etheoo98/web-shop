using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Services
{
    public class ShoppingCartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddToCart(int productId, int quantity)
        {
            var session = GetSession();
            var cart = GetCartItems();

            var existingItem = cart.FirstOrDefault(item => item.Id == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new ShoppingCartItem
                {
                    Id = productId,
                    Quantity = quantity
                });
            }

            session.SetString("cart", JsonConvert.SerializeObject(cart));
        }

        public List<ShoppingCartItem> GetCartItems()
        {
            var session = GetSession();

            var cartJson = session.GetString("cart");

            if (cartJson != null)
            {
                return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(cartJson);
            }
            else
            {
                return new List<ShoppingCartItem>();
            }
        }

        public void RemoveCartItem(int productId)
        {
            var session = GetSession();
            var cart = GetCartItems();
            var itemToRemove = cart.FirstOrDefault(item => item.Id == productId);

            if(itemToRemove != null)
            {
                cart.Remove(itemToRemove);

                session.SetString("cart", JsonConvert.SerializeObject(cart));
            }            
        }

        private ISession GetSession()
        {
            return _httpContextAccessor.HttpContext.Session;
        }
    }
}
