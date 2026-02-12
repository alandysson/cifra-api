using Microsoft.EntityFrameworkCore;
using controleDeGastos.Domain.Entities;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Infrastructure.Data;

namespace controleDeGastos.Infrastructure.Repositories;

public class ReceitaRepository : Repository<Receita>, IReceitaRepository
{
    public ReceitaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Receita>> GetByMesIdAsync(int mesId)
    {
        return await _dbSet
            .Where(r => r.MesId == mesId)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalByMesIdAsync(int mesId)
    {
        return await _dbSet
            .Where(r => r.MesId == mesId)
            .SumAsync(r => r.Valor);
    }
}
