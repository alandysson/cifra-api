using controleDeGastos.Domain.Entities;

namespace controleDeGastos.Domain.Interfaces;

public interface ICategoriaRepository : IRepository<CategoriaDespesa>
{
    Task<IEnumerable<CategoriaDespesa>> GetAllWithSubCategoriasAsync();
    Task<CategoriaDespesa?> GetByNomeAsync(string nome);
}
