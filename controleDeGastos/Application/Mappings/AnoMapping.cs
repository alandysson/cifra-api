using controleDeGastos.Domain.Entities;
using controleDeGastos.Application.DTOs;

namespace controleDeGastos.Application.Mappings;

public static class AnoMapping
{
    public static AnoDto ToDto(this Ano ano)
        => new(ano.AnoId, ano.Numero);

    public static AnoComMesesDto ToComMesesDto(this Ano ano)
        => new(
            ano.AnoId,
            ano.Numero,
            ano.Meses.Select(m => m.ToResumoDto()).OrderBy(m => m.Numero).ToList()
        );
}
