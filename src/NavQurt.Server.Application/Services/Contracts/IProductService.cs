namespace NavQurt.Server.Application.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<GetProductDto>> GetAllAsync();
    Task<GetProductDto?> GetByIdAsync(long id);
    Task<CreateProductDto> CreateAsync(CreateProductDto dto);
    Task UpdateAsync(UpdateProductDto dto);
    Task DeleteAsync(long id);
}