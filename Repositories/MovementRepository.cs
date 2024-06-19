using Microsoft.EntityFrameworkCore;

public class MovementRepository : IMovementRepository
{
    private readonly ApplicationDbContext _context;

    public MovementRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movement>> GetAllAsync()
    {
        return await _context.Movements.ToListAsync();
    }

    public async Task<Movement> GetByIdAsync(int id)
    {
        var movement = await _context.Movements.FindAsync(id);
        if (movement == null)
        {
            throw new KeyNotFoundException($"Movement with id {id} was not found.");
        }
        return movement;
    }

    public async Task AddAsync(Movement movement)
    {
        var product = await _context.Products.FindAsync(movement.ProductId);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with id {movement.ProductId} does not exist.");
        }

        if (movement.Type == "exit" && movement.Quantity > product.Quantity)
        {
            throw new ArgumentException($"Invalid quantity for type '{movement.Type}' ({product.Quantity} remaining)");
        }
        product.Quantity += movement.Quantity;
        _context.Movements.Add(movement);
        await _context.SaveChangesAsync();
    }
}