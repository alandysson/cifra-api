using controleDeGastos.Domain.Entities;
using controleDeGastos.Application.DTOs;

namespace controleDeGastos.Application.Mappings;

public static class SaldoMapping
{
    public static SaldoDto ToDto(this Saldo saldo)
        => new(
            saldo.SaldoId,
            saldo.MesId,
            saldo.Receita,
            saldo.Investimentos,
            saldo.DespesasFixas,
            saldo.DespesasVariaveis,
            saldo.DespesasExtras,
            saldo.DespesasAdicionais,
            saldo.Balanco,
            saldo.Observacao
        );
}
