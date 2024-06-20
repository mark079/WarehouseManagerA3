using AutoMapper;

public class MovementService : IMovementService
{
    private readonly IMovementRepository _movementRepository;
    private readonly IMapper _mapper;

    public MovementService(IMovementRepository movementRepository, IMapper mapper)
    {
        _movementRepository = movementRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MovementDto>> GetAllAsync()
    {
        var movements = await _movementRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<MovementDto>>(movements);
    }

    public async Task<MovementDto> GetByIdAsync(int id)
    {
        var movement = await _movementRepository.GetByIdAsync(id);
        return _mapper.Map<MovementDto>(movement);
    }

    public async Task AddAsync(CreateMovementDto createMovementDto)
    {
        if (createMovementDto.Type != "entry" && createMovementDto.Type != "exit")
        {
            throw new ArgumentException($"Invalid type '{createMovementDto.Type}'. Type must be 'entry' or 'exit'.");
        }

        if (createMovementDto.Quantity <= 0)
        {
            throw new ArgumentException("The quantity must be greater than zero");
        }
        var movement = _mapper.Map<Movement>(createMovementDto);
        await _movementRepository.AddAsync(movement);
    }
}