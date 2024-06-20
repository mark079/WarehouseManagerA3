using AutoMapper;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task AddAsync(CreateCategoryDto createCategoryDto)
    {
        var category = _mapper.Map<Category>(createCategoryDto);
        await _categoryRepository.AddAsync(category);
    }

    public async Task UpdateAsync(int id, CreateCategoryDto updateCategoryDto)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            throw new KeyNotFoundException("Category not found");
        }

        _mapper.Map(updateCategoryDto, category);
        await _categoryRepository.UpdateAsync(category);
    }

    public async Task DeleteAsync(int id)
    {
        await _categoryRepository.DeleteAsync(id);
    }

    public async Task<bool> CanDeleteCategoryAsync(int categoryId)
    {
        return !await _categoryRepository.CategoryHasProductsAsync(categoryId);
    }
}