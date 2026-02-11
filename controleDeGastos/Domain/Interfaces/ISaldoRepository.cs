using controleDeGastos.Domain.Entities;

namespace controleDeGastos.Domain.Interfaces;

public interface ISaldoRepository : IRepository<Saldo>
{
    Task<Saldo?> GetByMesIdAsync(int mesId);
}
