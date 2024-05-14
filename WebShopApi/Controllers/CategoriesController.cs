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
public class CategoriesController(ApplicationDbContext context, IMapper mapper) : ControllerBase
{
    //
    // GET Fetches all Categories
    //
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await context.Categories.ToListAsync();
        var categoryDtos = mapper.Map<List<CategoryDto>>(categories);
        
        return Ok(categoryDtos);
    }
    
    //
    // Creates a new Category
    //
    [HttpPost]
    public async Task<IActionResult> Post(CreateCategoryDto dto)
    {
        if (!ModelState.IsValid) return BadRequest("Missing property values");

        var alreadyExists = context.Categories.Any(c => c.Name == dto.Name);

        if (alreadyExists) return BadRequest("Category name already exists");
        
        var category = mapper.Map<Category>(dto);
        
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();

        return Created();
    }
}