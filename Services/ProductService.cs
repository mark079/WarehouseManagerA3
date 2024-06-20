using AutoMapper;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;


    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task AddAsync(CreateProductDto createProductDto)
    {
        if (createProductDto.Quantity < 0)
        {
            throw new ArgumentException("Quantity cannot be less than zero");
        }
        var category = await _categoryRepository.GetByIdAsync(createProductDto.CategoryId);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with id {createProductDto.CategoryId} was not found.");
        }
        var product = _mapper.Map<Product>(createProductDto);
        await _productRepository.AddAsync(product);
    }

    public async Task UpdateAsync(int id, UpdateProductDto updateProductDto)
    {
        var product = _mapper.Map<Product>(updateProductDto);
        product.Id = id;
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }

    public async Task<bool> CanDeleteProductAsync(int productId)
    {
        return !await _productRepository.ProductHasMovementsAsync(productId);
    }
}