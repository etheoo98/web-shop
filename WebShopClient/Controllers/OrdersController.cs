using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using WebShopClient.Models.RequestModels;
using WebShopClient.Models.ResponseModels;
using WebShopClient.Services;
using WebShopClient.ViewModels;

namespace WebShopClient.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ShoppingCartService _shoppingCartService;
        private readonly CustomerService _customService;
        private readonly ApiServices _apiServices;

        public OrdersController(
            OrderService orderService,
            ShoppingCartService shoppingCartService,
            CustomerService customerService,
            ApiServices apiServices)
        {
            _orderService = orderService;
            _shoppingCartService = shoppingCartService;
            _customService = customerService;
            _apiServices = apiServices;
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

            var userId = GetUserIdFromClaims();
            if (userId == null)
            {
                return Unauthorized();
            }

            var customer = await _customService.GetCustomerByIdAsync(userId.Value);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

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
        [Authorize]
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
                    DeliveryDate = DateTime.Now.AddDays(5),
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
                    IsPaid = true,
                    TotalSum = viewModel.TotalSum,
                    OrderItems = orderItems,
                    ShipmentDetails = shipmentDetails
                };

                var orderId = await _orderService.CreateOrderAsync(order);

                if (orderId)
                {
                    return RedirectToAction(nameof(OrderConfirmation));
                }
            }

            return View("Checkout", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> OrderConfirmation()
        {
            var userId = GetUserIdFromClaims();
            var customer = await _customService.GetCustomerByIdAsync(userId.Value);

            var latestOrder = customer.Orders
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefault();

            if (latestOrder == null)
            {
                return NotFound();
            }

            var viewModel = new OrderConfirmationViewModel
            {
                OrderId = latestOrder.Id,
                OrderDate = latestOrder.OrderDate,
                OrderItems = latestOrder.OrderProducts.Select(op => new OrderItemViewModel
                {
                    ProductName = op.ProductName,
                    Price = op.DiscountedPrice != 0 ? op.DiscountedPrice : op.Price,
                    Quantity = op.Quantity
                }).ToList(),
                TotalSum = latestOrder.OrderProducts.Sum(op => op.DiscountedPrice != 0 ? op.DiscountedPrice * op.Quantity :
                    op.Quantity * op.Product.Price),
                ShippingAddress = new ShippingAddressViewModel
                {
                    FirstName = latestOrder.Shipment.ShippingAddress.FirstName,
                    LastName = latestOrder.Shipment.ShippingAddress.LastName,
                    Email = latestOrder.Shipment.ShippingAddress.Email,
                    Phone = latestOrder.Shipment.ShippingAddress.Phone,
                    Street = latestOrder.Shipment.ShippingAddress.Street,
                    PostalCode = latestOrder.Shipment.ShippingAddress.PostalCode,
                    City = latestOrder.Shipment.ShippingAddress.City,
                    Country = latestOrder.Shipment.ShippingAddress.Country
                },
                DeliveryDate = latestOrder.Shipment.DeliveryDate
            };

            _shoppingCartService.EmptyCart();

            return View(viewModel);
        }

        private int? GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst("user-id");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return null;
            }
            return userId;
        }
    }
}
