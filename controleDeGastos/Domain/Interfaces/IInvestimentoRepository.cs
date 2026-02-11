using controleDeGastos.Domain.Entities;

namespace controleDeGastos.Domain.Interfaces;

public interface IInvestimentoRepository : IRepository<Investimento>
{
    Task<IEnumerable<Investimento>> GetByMesIdAsync(int mesId);
    Task<decimal> GetTotalByMesIdAsync(int mesId);
}
