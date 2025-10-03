using Microsoft.EntityFrameworkCore;
using NavQurt.Server.Application.Interfaces;
using NavQurt.Server.Domain.Entities;

namespace NavQurt.Server.Infrastructure.Persistence.Repositories;

public class ProductCategoryRepository
    : GenericRepository<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<ProductCategory>> GetCategoriesInSaleAsync()
    {
        return await _dbSet.Where(pc => pc.InSale && !pc.IsDeleted).ToListAsync();
    }
}