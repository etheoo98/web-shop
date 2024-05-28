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
public class CustomersController(ApplicationDbContext context, IMapper mapper) : BaseController
{
    //
    // Fetches all Customers
    //
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var customers = await context.Customers
             .Include(c => c.Address)          
            .ToListAsync();

        var customerDtos = mapper.Map<List<CustomerDto>>(customers);

        return Ok(customerDtos);
    }
    //
    // Fetches Customer by ID
    //
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        if (id <= 0) id = GetUserId();
        
        var customer = await context.Customers
            .Include(c => c.CustomerOrders)
            .ThenInclude(co => co.Order)
            .ThenInclude(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .ThenInclude(p => p.ProductCategories)!
            .ThenInclude(pc => pc.Category)
            .Include(c => c.Address)
            .Include(c => c.CustomerOrders)
            .ThenInclude(co => co.Order) 
            .ThenInclude(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .ThenInclude(p => p.Discount)                
            .Include(c => c.CustomerOrders)
            .ThenInclude(co => co.Order)
            .ThenInclude(o => o.ShipmentDetails)
            .ThenInclude(sd => sd.ShippingAddress)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null) return NotFound();

        var customerDto = mapper.Map<CustomerDto>(customer);

        return Ok(customerDto);
    }

    //
    // Creates a new Customer
    //
    [HttpPost]
    public async Task<IActionResult> Post(CreateCustomerDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var emailInUse = context.Customers.Any(c => c.Email == dto.Email);

        if (emailInUse) return Conflict("Email is already in use");

        var customer = mapper.Map<Customer>(dto);
        customer.Role = Role.Customer.ToString();

        await context.Customers.AddAsync(customer);
        await context.SaveChangesAsync();

        return Created();
    }

    //
    // Creates a new Address
    //
    [HttpPost("{id:int}/addresses")]
    public async Task<IActionResult> Post(int id, CreateAddressDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var address = mapper.Map<Address>(dto);
        address.FkCustomerId = id;

        await context.Addresses.AddAsync(address);
        await context.SaveChangesAsync();

        return Created();
    }

    //
    // Update a Customer
    //
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateCustomerDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var customer = await context.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        mapper.Map(dto, customer);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!context.Customers.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
}

