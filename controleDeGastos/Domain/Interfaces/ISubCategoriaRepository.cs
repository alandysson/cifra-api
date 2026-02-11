using controleDeGastos.Domain.Entities;

namespace controleDeGastos.Domain.Interfaces;

public interface ISubCategoriaRepository : IRepository<SubCategoriaDespesa>
{
    Task<IEnumerable<SubCategoriaDespesa>> GetByCategoriaIdAsync(int categoriaId);
    Task<SubCategoriaDespesa?> GetByIdWithCategoriaAsync(int id);
    Task<SubCategoriaDespesa?> GetByNomeAndCategoriaIdAsync(string nome, int categoriaId);
}
