using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebShop.Data;
using WebShop.Models.RequestDTOs;

namespace WebShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticatorsController(ApplicationDbContext context) : ControllerBase
{
    //
    // Attempt Login and return JWT token upon success
    //
    [HttpPost]
    public async Task<IActionResult> Login(LoginCustomerDto dto)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        var customer = await context.Customers.SingleOrDefaultAsync(c => c.Email == dto.Email);
        if (customer == null) return NotFound();

        var isAuthenticated = dto.Email == customer.Email && dto.Password == customer.Password;
        if (!isAuthenticated) return Unauthorized();

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SIGNING_KEY")!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.Role, customer.Role)
        };

        var token = new JwtSecurityToken(Environment.GetEnvironmentVariable("VALID_ISSUER"), Environment.GetEnvironmentVariable("VALID_AUDIENCE"),
            claims,
            expires: DateTime.Now.AddMonths(6),
            signingCredentials: credentials);
        
        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }
}