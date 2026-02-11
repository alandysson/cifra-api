using controleDeGastos.Domain.Entities;

namespace controleDeGastos.Domain.Interfaces;

public interface IMesRepository : IRepository<Mes>
{
    Task<IEnumerable<Mes>> GetByAnoIdAsync(int anoId);
    Task<Mes?> GetWithDetalhesAsync(int mesId);
    Task<Mes?> GetWithAnoAsync(int mesId);
    Task<IEnumerable<Mes>> GetByUsuarioIdAndRangeAsync(int usuarioId, int anoInicio, int mesInicio, int anoFim, int mesFim);
}
