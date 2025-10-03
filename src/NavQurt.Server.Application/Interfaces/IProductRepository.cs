using NavQurt.Server.Domain.Entities;

namespace NavQurt.Server.Application.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    // Qo‘shimcha metodlar: masalan, ombordagi mahsulotlarni olish
    Task<IEnumerable<Product>> GetProductsByWarehouseAsync(long warehouseId);
    Task<IEnumerable<Product>> GetProductsInSaleAsync();
}
