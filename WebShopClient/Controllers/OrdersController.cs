using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;
using WebShopClient.Services;
using WebShopClient.ViewModels;

namespace WebShopClient.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ShoppingCartService _shoppingCartService;
        private readonly CustomerService _customService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdersController(
            OrderService orderService,
            ShoppingCartService shoppingCartService,
            CustomerService customerService,
            IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _shoppingCartService = shoppingCartService;
            _customService = customerService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Checkout()
        {           
            var cartItems = _shoppingCartService.GetCartItems();

            if (cartItems == null || cartItems.Count == 0)
            {
                return RedirectToAction("GetCartItems", "ShoppingCarts");
            }

            var customer = await _customService.GetCustomerByIdAsync(1);

            var viewModel = new CheckoutViewModel
            {
                Customer = customer,
                CartItems = cartItems,
                TotalSum = cartItems.Sum(item => item.Price * item.Quantity),
                ShipmentDetails = new ShipmentDetailsViewModel
                {
                    ShippingAddress = new ShippingAddressViewModel
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        Phone = customer.Address.Phone,
                        Street = customer.Address.Street,
                        PostalCode = customer.Address.PostalCode,
                        City = customer.Address.City,
                        Country = customer.Address.Country
                    }
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel viewModel)
        {           
            if (ModelState.IsValid)
            {
                var orderItems = viewModel.CartItems.Select(item => new CreateOrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList();

                var shipmentDetails = new Shipment
                {
                    ShippedDate = DateTime.Now,
                    ShippingAddress = new ShippingAddress
                    {
                        FirstName = viewModel.ShipmentDetails.ShippingAddress.FirstName,
                        LastName = viewModel.ShipmentDetails.ShippingAddress.LastName,
                        Email = viewModel.ShipmentDetails.ShippingAddress.Email,
                        Phone = viewModel.ShipmentDetails.ShippingAddress.Phone,
                        Street = viewModel.ShipmentDetails.ShippingAddress.Street,
                        PostalCode = viewModel.ShipmentDetails.ShippingAddress.PostalCode,
                        City = viewModel.ShipmentDetails.ShippingAddress.City,
                        Country = viewModel.ShipmentDetails.ShippingAddress.Country
                    }
                };

                var order = new CreateOrder
                {
                    CustomerId = viewModel.Customer.Id,
                    OrderItems = orderItems,
                    ShipmentDetails = shipmentDetails
                };

                var orderId = await _orderService.CreateOrderAsync(order);

                if (orderId.HasValue)
                {
                    return RedirectToAction("OrderConfirmation", new { orderId = orderId.Value });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "There was a problem placing the order. Please try again.");
                }
            }

            return View("Checkout", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> OrderConfirmation(int orderId)
        {            
            var order = await _orderService.GetOrderByIdAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            var viewModel = new OrderConfirmationViewModel
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                OrderItems = order.Products.Select(p => new OrderItemViewModel
                {
                    ProductName = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList(),
                ShippingAddress = new ShippingAddressViewModel
                {
                    FirstName = order.Shipment.ShippingAddress.FirstName,
                    LastName = order.Shipment.ShippingAddress.LastName,
                    Email = order.Shipment.ShippingAddress.Email,
                    Phone = order.Shipment.ShippingAddress.Phone,
                    Street = order.Shipment.ShippingAddress.Street,
                    PostalCode = order.Shipment.ShippingAddress.PostalCode,
                    City = order.Shipment.ShippingAddress.City,
                    Country = order.Shipment.ShippingAddress.Country
                },
                DeliveryDate = order.Shipment.DeliveryDate
            };

            _shoppingCartService.EmptyCart();

            return View(order);
        }
    }
}

//var token = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");

//if (string.IsNullOrEmpty(token))
//{
//    return RedirectToAction("Customers", "Login");
//}
