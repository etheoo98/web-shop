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
        var order = await context.Orders
            .Include(o => o.CustomerOrders)
            .ThenInclude(co => co.Customer)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .ThenInclude(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .ToListAsync();

        var orderDtos = mapper.Map<List<OrderDto>>(order);

        return Ok(orderDtos);
    }

    //
    // Creates a new Order
    //
    [HttpPost]
    public async Task<IActionResult> Post(CreateOrderDto dto)
    {
        // Validation
        if (!ModelState.IsValid || dto.ProductIds.Count == 0) return BadRequest("Missing property values");
        
        var customerExists = context.Customers.Any(c => c.Id == dto.CustomerId);
        if (!customerExists) return BadRequest("Invalid customer ID");
        
        var productsExist = context.Products.All(p => dto.ProductIds.Contains(p.Id));
        if (!productsExist) return BadRequest("Invalid product IDs");
        
        // Begin transaction
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var order = mapper.Map<Order>(dto);

            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            
            foreach (var productId in dto.ProductIds)
            {
                var orderProducts = new OrderProducts()
                {
                    FkProductId = productId,
                    FkOrderId = order.Id
                };

                await context.OrderProducts.AddAsync(orderProducts);
            }
            
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return Created();
        }
        catch
        {
            await transaction.RollbackAsync();
            return StatusCode(500);
        }
    }
}