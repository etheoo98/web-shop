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
        
        await context.Customers.AddAsync(customer);
        await context.SaveChangesAsync();

        return Created();
    }
}