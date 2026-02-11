using Microsoft.EntityFrameworkCore;
using controleDeGastos.Domain.Entities;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Infrastructure.Data;

namespace controleDeGastos.Infrastructure.Repositories;

public class DespesaRepository : Repository<Despesa>, IDespesaRepository
{
    public DespesaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Despesa>> GetByMesIdAsync(int mesId)
    {
        return await _dbSet
            .Include(d => d.TipoDespesa)
            .Include(d => d.SubCategoria)
                .ThenInclude(s => s.Categoria)
            .Where(d => d.MesId == mesId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Despesa>> GetByMesIdAndTipoAsync(int mesId, int tipoDespesaId)
    {
        return await _dbSet
            .Include(d => d.SubCategoria)
                .ThenInclude(s => s.Categoria)
            .Where(d => d.MesId == mesId && d.TipoDespesaId == tipoDespesaId)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalByMesIdAndTipoAsync(int mesId, int tipoDespesaId)
    {
        return await _dbSet
            .Where(d => d.MesId == mesId && d.TipoDespesaId == tipoDespesaId)
            .SumAsync(d => d.Valor);
    }
}
