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
		private readonly ProductService _productServices;

		public ShoppingCartService(IHttpContextAccessor httpContextAccessor, ProductService productServices)
		{
			_httpContextAccessor = httpContextAccessor;
			_productServices = productServices;
		}

		public async Task AddToCartAsync(int productId, int quantity)
		{
			var session = GetSession();
			var cart = GetCartItems();
			var existingItem = cart.Find(item => item.ProductId == productId);

			if (existingItem != null)
			{
				existingItem.Quantity += quantity;
			}
			else
			{
				var product = await _productServices.GetProductAsync(productId);

				if (product != null)
				{
					cart.Add(new ShoppingCartItem
					{
						ProductId = product.Id,
						ProductName = product.Name,
						Price = product.Price,
						Quantity = quantity,	
						StockQuantity = product.Quantity,
						DiscountedPrice = product.Discount.DiscountedPrice
					});
				}
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

		public void UpdateItemQuantity(int productId, int quantity)
		{
			var session = GetSession();
			var cart = GetCartItems();
			var item = cart.Find(item => item.ProductId == productId);

			if (item != null)
			{
				item.Quantity = quantity;
				session.SetString("cart", JsonConvert.SerializeObject(cart));
			}
		}

		public void RemoveCartItem(int productId)
		{
			var session = GetSession();
			var cart = GetCartItems();
			var itemToRemove = cart.Find(item => item.ProductId == productId);

			if (itemToRemove != null)
			{
				cart.Remove(itemToRemove);
				session.SetString("cart", JsonConvert.SerializeObject(cart));
			}
		}

		public void EmptyCart()
		{
			var session = GetSession();
			session.Remove("cart");	
		}

		private ISession GetSession()
		{
			return _httpContextAccessor.HttpContext.Session;
		}
	}
}
