using Microsoft.EntityFrameworkCore;
using controleDeGastos.Domain.Entities;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Infrastructure.Data;

namespace controleDeGastos.Infrastructure.Repositories;

public class CategoriaRepository : Repository<CategoriaDespesa>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CategoriaDespesa>> GetAllWithSubCategoriasAsync()
    {
        return await _dbSet
            .Include(c => c.SubCategorias)
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<CategoriaDespesa?> GetByNomeAsync(string nome)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.Nome.ToLower() == nome.ToLower());
    }
}
