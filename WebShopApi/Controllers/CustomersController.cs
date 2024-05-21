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
public class CustomersController(ApplicationDbContext context, IMapper mapper) : ControllerBase
{
    //
    // Fetches all Customers
    //
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        if (!ModelState.IsValid) return BadRequest("Missing property values");
        
        var customers = await context.Customers.ToListAsync();
        var customerDtos = mapper.Map<List<CustomerDto>>(customers);

        return Ok(customerDtos);
    }

    // Fetches Customer by Id
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var customer = await context.Customers.FindAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        var customerDto = mapper.Map<CustomerDto>(customer);
        return Ok(customerDto);
    }

    //
    // Creates a new Customer
    //
    [HttpPost]
    public async Task<IActionResult> Post(CreateCustomerDto dto)
    {
        if (!ModelState.IsValid) return BadRequest("Missing property values");

        var emailInUse = context.Customers.Any(c => c.Email == dto.Email);

        if (emailInUse) return BadRequest("Email is already in use");
        
        var customer = mapper.Map<Customer>(dto);
        customer.Role = Role.Customer.ToString();
        
        await context.Customers.AddAsync(customer);
        await context.SaveChangesAsync();

        return Created();
    }

    // Update an Customer

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateCustomerDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid data");
        }

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

