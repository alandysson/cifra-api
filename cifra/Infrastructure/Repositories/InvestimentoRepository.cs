using Microsoft.EntityFrameworkCore;
using controleDeGastos.Domain.Entities;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Infrastructure.Data;

namespace controleDeGastos.Infrastructure.Repositories;

public class InvestimentoRepository : Repository<Investimento>, IInvestimentoRepository
{
    public InvestimentoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Investimento>> GetByMesIdAsync(int mesId)
    {
        return await _dbSet
            .Where(i => i.MesId == mesId)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalByMesIdAsync(int mesId)
    {
        return await _dbSet
            .Where(i => i.MesId == mesId)
            .SumAsync(i => i.Valor);
    }
}
