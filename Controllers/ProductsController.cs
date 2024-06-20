using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var categories = await _productService.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        try
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateProductDto createProductDto)
    {
        try
        {
            await _productService.AddAsync(createProductDto);
            return CreatedAtAction(nameof(GetById), new { id = createProductDto.Name }, createProductDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateProductDto updateProductDto)
    {
        await _productService.UpdateAsync(id, updateProductDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var canDelete = await _productService.CanDeleteProductAsync(id);
        if (!canDelete)
        {
            return BadRequest("Cannot delete product because there are linked movements");
        }

        try
        {
            await _productService.GetByIdAsync(id);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        await _productService.DeleteAsync(id);
        return NoContent();
    }
}