using NavQurt.Server.Domain.Entities;

namespace NavQurt.Server.Application.Interfaces;

public interface IProductCategoryRepository : IGenericRepository<ProductCategory>
{
    // Qo‘shimcha maxsus metodlar kerak bo‘lsa shu yerda yoziladi
    Task<IEnumerable<ProductCategory>> GetCategoriesInSaleAsync();
}
