using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MovementsController : ControllerBase
{
    private readonly IMovementService _movementService;

    public MovementsController(IMovementService movementService)
    {
        _movementService = movementService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovementDto>>> GetAllMovements()
    {
        var movements = await _movementService.GetAllAsync();
        return Ok(movements);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovementDto>> GetMovementById(int id)
    {
        try
        {
            var movement = await _movementService.GetByIdAsync(id);
            return Ok(movement);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddMovement(CreateMovementDto createMovementDto)
    {
        try
        {
            await _movementService.AddAsync(createMovementDto);
            return Ok("Movement added successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}