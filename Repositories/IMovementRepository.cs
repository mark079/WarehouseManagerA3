public interface IMovementRepository
{
    Task<IEnumerable<Movement>> GetAllAsync();
    Task<Movement> GetByIdAsync(int id);
    Task AddAsync(Movement movement);
}