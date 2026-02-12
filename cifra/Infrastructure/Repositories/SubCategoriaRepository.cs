using Microsoft.EntityFrameworkCore;
using controleDeGastos.Domain.Entities;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Infrastructure.Data;

namespace controleDeGastos.Infrastructure.Repositories;

public class SubCategoriaRepository : Repository<SubCategoriaDespesa>, ISubCategoriaRepository
{
    public SubCategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<SubCategoriaDespesa>> GetByCategoriaIdAsync(int categoriaId)
    {
        return await _dbSet
            .Include(s => s.Categoria)
            .Where(s => s.CategoriaId == categoriaId)
            .OrderBy(s => s.Nome)
            .ToListAsync();
    }

    public async Task<SubCategoriaDespesa?> GetByIdWithCategoriaAsync(int id)
    {
        return await _dbSet
            .Include(s => s.Categoria)
            .FirstOrDefaultAsync(s => s.SubCategoriaId == id);
    }

    public async Task<SubCategoriaDespesa?> GetByNomeAndCategoriaIdAsync(string nome, int categoriaId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(s => s.CategoriaId == categoriaId && s.Nome.ToLower() == nome.ToLower());
    }
}
