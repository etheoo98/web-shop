using System.Text.Json;
using WebShopClient.Models.ResponseModels;

namespace WebShopClient.Services
{
	public class ShoppingCartService(IHttpContextAccessor httpContextAccessor, ProductService productServices)
	{
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
				var product = await productServices.GetProductAsync(productId);

				if (product != null)
				{
					cart.Add(new ShoppingCartItem
					{
						ProductId = product.Id,
						ProductName = product.Name,
						Price = product.Price,
						Quantity = quantity,	
						StockQuantity = product.Quantity,
						DiscountedPrice = product.Discount?.DiscountedPrice ?? 0
					});
				}
			}

			session.SetString("cart", JsonSerializer.Serialize(cart));
		}

		public List<ShoppingCartItem> GetCartItems()
		{
			var session = GetSession();
			var cartJson = session.GetString("cart");

			if (cartJson != null)
			{
				return JsonSerializer.Deserialize<List<ShoppingCartItem>>(cartJson);
			}
			
			return [];
		}

		public void UpdateItemQuantity(int productId, int quantity)
		{
			var session = GetSession();
			var cart = GetCartItems();
			var item = cart.Find(item => item.ProductId == productId);
			if (item == null) return;
			item.Quantity = quantity;
			session.SetString("cart", JsonSerializer.Serialize(cart));
		}

		public void RemoveCartItem(int productId)
		{
			var session = GetSession();
			var cart = GetCartItems();
			var itemToRemove = cart.Find(item => item.ProductId == productId);
			if (itemToRemove == null) return;
			cart.Remove(itemToRemove);
			session.SetString("cart", JsonSerializer.Serialize(cart));
		}

		public void EmptyCart()
		{
			var session = GetSession();
			session.Remove("cart");	
		}

		private ISession GetSession()
		{
			return httpContextAccessor.HttpContext.Session;
		}
	}
}
