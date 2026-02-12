using Microsoft.EntityFrameworkCore;
using controleDeGastos.Domain.Entities;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Infrastructure.Data;

namespace controleDeGastos.Infrastructure.Repositories;

public class AnoRepository : Repository<Ano>, IAnoRepository
{
    public AnoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Ano?> GetByNumeroAsync(int numero)
    {
        return await _dbSet.FirstOrDefaultAsync(a => a.Numero == numero);
    }

    public async Task<Ano?> GetWithMesesAsync(int anoId)
    {
        return await _dbSet
            .Include(a => a.Meses)
            .FirstOrDefaultAsync(a => a.AnoId == anoId);
    }

    public async Task<IEnumerable<Ano>> GetByUsuarioIdAsync(int usuarioId)
    {
        return await _dbSet
            .Where(a => a.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task<Ano?> GetByNumeroAndUsuarioIdAsync(int numero, int usuarioId)
    {
        return await _dbSet.FirstOrDefaultAsync(a => a.Numero == numero && a.UsuarioId == usuarioId);
    }
}
