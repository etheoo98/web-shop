using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models.DbModels;
using WebShop.Models.RequestDTOs;

namespace WebShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountsController(ApplicationDbContext context, IMapper mapper) : ControllerBase
{
    //
    // Creates a Discount for a Product
    //
    [HttpPost]
    public async Task<IActionResult> Post(CreateDiscountDto dto)
    {
        // Validation
        if (!ModelState.IsValid) return BadRequest("Missing property values");

        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == dto.ProductId);
        if (product == null) return BadRequest($"Product with id \"{dto.ProductId}\" does not exist");
        if (product.FkDiscountId.HasValue) return BadRequest($"Product with id \"{dto.ProductId}\" already has a discount");
        if (dto.Percent is < 5 or > 95) return BadRequest($"Percent value \"{dto.Percent}\" must be \"5\" or greater and \"95\" or less"); 
        
        // Begin transaction
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var discount = mapper.Map<Discount>(dto);

            await context.Discounts.AddAsync(discount);
            await context.SaveChangesAsync();

            product.FkDiscountId = discount.Id;
            
            await context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok();
        }
        catch
        {
            await transaction.RollbackAsync();
            return StatusCode(500);
        }
    }
}