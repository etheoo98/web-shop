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
public class ProductsController(ApplicationDbContext context, IMapper mapper) : ControllerBase
{
    //
    // Fetches all Products
    //
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await context.Products
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.Discount)
            .ToListAsync();

        var productDtos = mapper.Map<List<ProductDto>>(products);

        return Ok(productDtos);
    }

    //
    // Fetches Products belonging to specific Categories
    //
    [HttpGet("Filter")]
    public async Task<IActionResult> Get([FromQuery(Name = "category")] string[] categories)
    {
        categories = categories.Select(c => c.ToLower()).ToArray();

        var products = await context.Products
            .Where(p => p.ProductCategories.Any(pc => categories.Contains(pc.Category.Name.ToLower())))
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.Discount)
            .ToListAsync();

        var productDtos = mapper.Map<List<ProductDto>>(products);

        return Ok(productDtos);
    }

    //
    // Fetch a specific Product
    //
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var products = await context.Products
            .Where(p => p.Id == id)
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.Discount)
            .FirstOrDefaultAsync();

        var productDto = mapper.Map<ProductDto>(products);

        return Ok(productDto);
    }

    //
    // Creates a new Product
    //
    [HttpPost]
    public async Task<IActionResult> Post(CreateProductDto dto)
    {
        // Validation
        if (!ModelState.IsValid || dto.CategoryIds.Count == 0) return BadRequest("Missing property values");

        var existingProduct = context.Products.Any(p => p.Name == dto.Name);
        if (existingProduct) return Conflict("Product name already exists");

        var allDbCategoryIds = await context.Categories.Select(c => c.Id).ToListAsync();
        var categoriesExist = dto.CategoryIds.All(id => allDbCategoryIds.Contains(id));

        if (!categoriesExist) return BadRequest("Category IDs provided does not exist");

        // Begin transaction
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var product = mapper.Map<Product>(dto);

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            foreach (var categoryId in dto.CategoryIds)
            {
                var productCategory = new ProductCategory
                {
                    FkProductId = product.Id,
                    FkCategoryId = categoryId
                };

                await context.ProductCategories.AddAsync(productCategory);
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

    //
    // Edit a Product
    //
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, UpdateProductDto dto)
    {
        // Validation
        if (!ModelState.IsValid) return BadRequest("Missing property values");

        var product = await context.Products
            .Where(p => p.Id == id)
            .Include(p => p.ProductCategories)
            .FirstOrDefaultAsync();

        if (product == null) return NotFound();

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Quantity = dto.Quantity;
        product.ProductCategories.Clear();

        foreach (var categoryId in dto.CategoryIds)
        {
            var productCategory = new ProductCategory
            {
                FkProductId = product.Id,
                FkCategoryId = categoryId
            };

            product.ProductCategories.Add(productCategory);
        }

        await context.SaveChangesAsync();

        return Ok();
    }

    //
    // Discontinue a Product
    //
    [HttpPut("{id:int}/discontinue")]
    public async Task<IActionResult> Discontinue(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null) return NotFound();

        product.IsDiscontinued = true;
        await context.SaveChangesAsync();

        return Ok();
    }
}