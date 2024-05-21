using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using WebShop.Data;
using WebShop.Models.DbModels;
using WebShop.Models.RequestDTOs;
using WebShop.Models.ResponseDTOs;

namespace WebShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public OrdersController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    //
    // Fetches all Orders
    //
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var customerOrders = await _context.CustomerOrders
                .Include(co => co.Order)
                .ThenInclude(o => o.Customer)
                .Include(co => co.Order)
                .ThenInclude(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ThenInclude(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Include(co => co.Order)
                .ThenInclude(o => o.ShipmentDetails)
                .ThenInclude(sd => sd.ShippingAddress)
                .ToListAsync();

            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(customerOrders.Select(co => co.Order));

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
        try
        {
            // Validation
            if (!ModelState.IsValid || createdOrderDto.OrderItems.Count == 0)
                return BadRequest("Missing or invalid property values");
            
            var order = _mapper.Map<Order>(createdOrderDto);
            
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            var customerOrder = new CustomerOrder
            {
                FkCustomerId = createdOrderDto.CustomerId,
                FkOrderId = order.Id
            };
            await _context.CustomerOrders.AddAsync(customerOrder);
            await _context.SaveChangesAsync();

            // If there are order items, map and save them to the database
            if (createdOrderDto.OrderItems != null && createdOrderDto.OrderItems.Count != 0)
            {
                var orderProducts = _mapper.Map<List<OrderProducts>>(createdOrderDto.OrderItems);
                orderProducts.ForEach(op => op.FkOrderId = order.Id);
                await _context.OrderProducts.AddRangeAsync(orderProducts);
                await _context.SaveChangesAsync();
            }

            // If shipment details are provided, map and save them to the database
            if (createdOrderDto.ShipmentDetails?.ShippingAddress != null)
            {
                var shippingAddress = _mapper.Map<ShippingAddress>(createdOrderDto.ShipmentDetails.ShippingAddress);
                await _context.ShippingAddresses.AddAsync(shippingAddress);
                await _context.SaveChangesAsync();

                var shipment = new Shipment
                {
                    FkOrderId = order.Id,
                    FkShippingAddressId = shippingAddress.Id,
                    ShippedDate = createdOrderDto.ShipmentDetails.ShippedDate ?? DateTime.Now,
                    DeliveryDate = createdOrderDto.ShipmentDetails.DeliveryDate
                };
                await _context.Shipments.AddAsync(shipment);
                await _context.SaveChangesAsync();           
            }

            return Created();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
        }
    }
}