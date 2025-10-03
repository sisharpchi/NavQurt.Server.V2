using Microsoft.EntityFrameworkCore;
using NavQurt.Server.Application.Interfaces;
using NavQurt.Server.Domain.Entities;

namespace NavQurt.Server.Infrastructure.Persistence.Repositories;

public class WarehouseRepository
    : GenericRepository<Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Warehouse>> GetActiveWarehousesAsync()
    {
        return await _dbSet
            .Where(w => !w.IsDeleted)
            .ToListAsync();
    }
}