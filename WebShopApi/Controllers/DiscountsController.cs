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
public class DiscountsController(ApplicationDbContext context, IMapper mapper) : ControllerBase
{
    //
    // Creates a Discount for a Product
    //
    [HttpPost]
    public async Task<IActionResult> Post(CreateDiscountDto dto)
    {
        // Validation
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == dto.ProductId);
        if (product == null) return BadRequest($"Product with id \"{dto.ProductId}\" does not exist");
        if (product.FkDiscountId.HasValue) return BadRequest($"Product with id \"{dto.ProductId}\" already has a discount");
        
        // Begin transaction
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {

            var discount = mapper.Map<Discount>(dto);

            discount.DiscountedPrice = Math.Round(product.Price - (discount.Percent * product.Price / 100), 1);

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

    //
    // Fetches all Discounts
    //
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var discounts = await context.Discounts.ToListAsync();
        var discountDtos = mapper.Map<List<DiscountDto>>(discounts);

        return Ok(discountDtos);
    }

    //
    // Deletes a Discount by ID
    //
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        // Begin transaction
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var discount = await context.Discounts.FindAsync(id);
            if (discount == null) return NotFound($"Discount with id \"{id}\" does not exist");

            var product = await context.Products.FirstOrDefaultAsync(p => p.FkDiscountId == id);
            if (product != null)
            {
                product.FkDiscountId = null;
            }

            context.Discounts.Remove(discount);
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
