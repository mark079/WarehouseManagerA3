using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetById(int id)
    {
        try
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateCategoryDto createCategoryDto)
    {
        await _categoryService.AddAsync(createCategoryDto);
        return CreatedAtAction(nameof(GetById), new { id = createCategoryDto.Name }, createCategoryDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, CreateCategoryDto updateCategoryDto)
    {
        try
        {
            await _categoryService.UpdateAsync(id, updateCategoryDto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var canDelete = await _categoryService.CanDeleteCategoryAsync(id);
        if (!canDelete)
        {
            return BadRequest("Cannot delete category because there are linked products");
        }
        try
        {
            await _categoryService.GetByIdAsync(id);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        await _categoryService.DeleteAsync(id);
        return NoContent();
    }
}