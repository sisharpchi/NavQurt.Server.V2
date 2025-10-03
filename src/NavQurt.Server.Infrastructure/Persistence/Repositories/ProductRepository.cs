using Microsoft.EntityFrameworkCore;
using NavQurt.Server.Application.Interfaces;
using NavQurt.Server.Domain.Entities;

namespace NavQurt.Server.Infrastructure.Persistence.Repositories;

public class ProductRepository
    : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Product>> GetProductsByWarehouseAsync(long warehouseId)
    {
        return await _dbSet
            .Where(p => p.WarehouseId == warehouseId)
            .Include(p => p.ProductCategory)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsInSaleAsync()
    {
        return await _dbSet
            .Where(p => p.InSale)
            .Include(p => p.ProductCategory)
            .ToListAsync();
    }
}
