using NavQurt.Server.Domain.Entities;

namespace NavQurt.Server.Application.Interfaces;

public interface IWarehouseRepository : IGenericRepository<Warehouse>
{
    // Qo‘shimcha metodlar kerak bo‘lsa shu yerda yoziladi
    Task<IEnumerable<Warehouse>> GetActiveWarehousesAsync();
}
