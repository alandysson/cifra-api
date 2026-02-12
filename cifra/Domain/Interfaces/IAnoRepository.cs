using controleDeGastos.Domain.Entities;

namespace controleDeGastos.Domain.Interfaces;

public interface IAnoRepository : IRepository<Ano>
{
    Task<Ano?> GetByNumeroAsync(int numero);
    Task<Ano?> GetWithMesesAsync(int anoId);
    Task<IEnumerable<Ano>> GetByUsuarioIdAsync(int usuarioId);
    Task<Ano?> GetByNumeroAndUsuarioIdAsync(int numero, int usuarioId);
}
