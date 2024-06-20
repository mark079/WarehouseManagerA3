public interface IMovementService
{
    Task<IEnumerable<MovementDto>> GetAllAsync();
    Task<MovementDto> GetByIdAsync(int id);
    Task AddAsync(CreateMovementDto createMovementDto);
}