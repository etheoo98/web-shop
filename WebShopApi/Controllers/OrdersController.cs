using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models.DbModels;
using WebShop.Models.RequestDTOs;
using WebShop.Models.ResponseDTOs;

namespace WebShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(ApplicationDbContext context, IMapper mapper) : ControllerBase
{   
    //
    // Fetches all Orders
    //
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var orders = await context.Orders
                .Include(o => o.CustomerOrders)
                .ThenInclude(co => co.Customer)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ThenInclude(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Include(o => o.ShipmentDetails)
                .ThenInclude(sd => sd.ShippingAddress)
                .ToListAsync();

            var orderDtos = mapper.Map<IEnumerable<OrderDto>>(orders);

            return Ok(orderDtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
        }    
    }

    //
    // Creates a new Order
    //    
    [HttpPost]
    public async Task<IActionResult> Post(CreateOrderDto createdOrderDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        try
        {
            // Validation
            if (!ModelState.IsValid || createdOrderDto.OrderItems.Count == 0)
                return BadRequest("Missing or invalid property values");
            
            var order = mapper.Map<Order>(createdOrderDto);
            
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            var customerOrder = new CustomerOrder
            {
                FkCustomerId = createdOrderDto.CustomerId,
                FkOrderId = order.Id
            };
            await context.CustomerOrders.AddAsync(customerOrder);
            await context.SaveChangesAsync();

            // If there are order items, map and save them to the database
            if (createdOrderDto.OrderItems != null && createdOrderDto.OrderItems.Count != 0)
            {
                var orderProducts = mapper.Map<List<OrderProducts>>(createdOrderDto.OrderItems);
                orderProducts.ForEach(op => op.FkOrderId = order.Id);
                await context.OrderProducts.AddRangeAsync(orderProducts);
                await context.SaveChangesAsync();
            }

            // If shipment details are provided, map and save them to the database
            if (createdOrderDto.ShipmentDetails?.ShippingAddress != null)
            {
                var shippingAddress = mapper.Map<ShippingAddress>(createdOrderDto.ShipmentDetails.ShippingAddress);
                await context.ShippingAddresses.AddAsync(shippingAddress);
                await context.SaveChangesAsync();

                var shipment = new Shipment
                {
                    FkOrderId = order.Id,
                    FkShippingAddressId = shippingAddress.Id,
                    ShippedDate = createdOrderDto.ShipmentDetails.ShippedDate ?? DateTime.Now,
                    DeliveryDate = createdOrderDto.ShipmentDetails.DeliveryDate
                };
                await context.Shipments.AddAsync(shipment);
                await context.SaveChangesAsync();           
            }

            return Created();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
        }
    }
}