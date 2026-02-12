using controleDeGastos.Domain.Entities;

namespace controleDeGastos.Domain.Interfaces;

public interface IDespesaRepository : IRepository<Despesa>
{
    Task<IEnumerable<Despesa>> GetByMesIdAsync(int mesId);
    Task<IEnumerable<Despesa>> GetByMesIdAndTipoAsync(int mesId, int tipoDespesaId);
    Task<decimal> GetTotalByMesIdAndTipoAsync(int mesId, int tipoDespesaId);
}
