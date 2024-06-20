public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<CategoryDto> GetByIdAsync(int id);
    Task AddAsync(CreateCategoryDto createCategoryDto);
    Task UpdateAsync(int id, CreateCategoryDto createCategoryDto);
    Task DeleteAsync(int id);
    Task<bool> CanDeleteCategoryAsync(int categoryId);
}