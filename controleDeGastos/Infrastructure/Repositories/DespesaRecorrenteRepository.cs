using Microsoft.EntityFrameworkCore;
using controleDeGastos.Domain.Entities;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Infrastructure.Data;

namespace controleDeGastos.Infrastructure.Repositories;

public class DespesaRecorrenteRepository : Repository<DespesaRecorrente>, IDespesaRecorrenteRepository
{
    public DespesaRecorrenteRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<DespesaRecorrente>> GetByUsuarioIdAsync(int usuarioId)
    {
        return await _dbSet
            .Where(dr => dr.UsuarioId == usuarioId)
            .Include(dr => dr.TipoDespesa)
            .Include(dr => dr.SubCategoria)
                .ThenInclude(s => s.Categoria)
            .Include(dr => dr.Despesas)
                .ThenInclude(d => d.Mes)
                    .ThenInclude(m => m.Ano)
            .OrderBy(dr => dr.AnoInicio)
            .ThenBy(dr => dr.MesInicio)
            .ToListAsync();
    }

    public async Task<DespesaRecorrente?> GetByIdWithDespesasAsync(int id)
    {
        return await _dbSet
            .Include(dr => dr.TipoDespesa)
            .Include(dr => dr.SubCategoria)
                .ThenInclude(s => s.Categoria)
            .Include(dr => dr.Despesas)
                .ThenInclude(d => d.Mes)
                    .ThenInclude(m => m.Ano)
            .FirstOrDefaultAsync(dr => dr.DespesaRecorrenteId == id);
    }
}
