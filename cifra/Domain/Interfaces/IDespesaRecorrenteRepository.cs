using controleDeGastos.Domain.Entities;

namespace controleDeGastos.Domain.Interfaces;

public interface IDespesaRecorrenteRepository : IRepository<DespesaRecorrente>
{
    Task<IEnumerable<DespesaRecorrente>> GetByUsuarioIdAsync(int usuarioId);
    Task<DespesaRecorrente?> GetByIdWithDespesasAsync(int id);
}
