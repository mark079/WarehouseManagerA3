public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto> GetByIdAsync(int id);
    Task AddAsync(CreateProductDto createProductDto);
    Task UpdateAsync(int id, UpdateProductDto updateProductDto);
    Task DeleteAsync(int id);
    Task<bool> CanDeleteProductAsync(int productId);
}