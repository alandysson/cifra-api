using controleDeGastos.Domain.Entities;
using controleDeGastos.Application.DTOs;

namespace controleDeGastos.Application.Mappings;

public static class InvestimentoMapping
{
    public static InvestimentoDto ToDto(this Investimento investimento, decimal totalReceita)
        => new(
            investimento.InvestimentoId,
            investimento.MesId,
            investimento.Descricao,
            investimento.Valor,
            totalReceita > 0 ? Math.Round((investimento.Valor / totalReceita) * 100, 2) : null
        );
}
