using controleDeGastos.Domain.Entities;

namespace controleDeGastos.Domain.Interfaces;

public interface IReceitaRepository : IRepository<Receita>
{
    Task<IEnumerable<Receita>> GetByMesIdAsync(int mesId);
    Task<decimal> GetTotalByMesIdAsync(int mesId);
}
