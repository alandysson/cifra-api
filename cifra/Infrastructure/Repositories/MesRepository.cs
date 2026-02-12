using Microsoft.EntityFrameworkCore;
using controleDeGastos.Domain.Entities;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Infrastructure.Data;

namespace controleDeGastos.Infrastructure.Repositories;

public class MesRepository : Repository<Mes>, IMesRepository
{
    public MesRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Mes>> GetByAnoIdAsync(int anoId)
    {
        return await _dbSet
            .Where(m => m.AnoId == anoId)
            .OrderBy(m => m.Numero)
            .ToListAsync();
    }

    public async Task<Mes?> GetWithDetalhesAsync(int mesId)
    {
        return await _dbSet
            .Include(m => m.Ano)
            .Include(m => m.Receitas)
            .Include(m => m.Investimentos)
            .Include(m => m.Despesas)
                .ThenInclude(d => d.TipoDespesa)
            .Include(m => m.Despesas)
                .ThenInclude(d => d.SubCategoria)
                    .ThenInclude(s => s.Categoria)
            .Include(m => m.Saldo)
            .FirstOrDefaultAsync(m => m.MesId == mesId);
    }

    public async Task<Mes?> GetWithAnoAsync(int mesId)
    {
        return await _dbSet
            .Include(m => m.Ano)
            .FirstOrDefaultAsync(m => m.MesId == mesId);
    }

    public async Task<IEnumerable<Mes>> GetByUsuarioIdAndRangeAsync(int usuarioId, int anoInicio, int mesInicio, int anoFim, int mesFim)
    {
        return await _dbSet
            .Include(m => m.Ano)
            .Where(m => m.Ano.UsuarioId == usuarioId)
            .Where(m =>
                (m.Ano.Numero > anoInicio || (m.Ano.Numero == anoInicio && m.Numero >= mesInicio)) &&
                (m.Ano.Numero < anoFim || (m.Ano.Numero == anoFim && m.Numero <= mesFim))
            )
            .OrderBy(m => m.Ano.Numero)
            .ThenBy(m => m.Numero)
            .ToListAsync();
    }
}
