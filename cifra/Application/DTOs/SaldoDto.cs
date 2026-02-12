namespace controleDeGastos.Application.DTOs;

public record SaldoDto(
    int SaldoId,
    int MesId,
    decimal Receita,
    decimal Investimentos,
    decimal DespesasFixas,
    decimal DespesasVariaveis,
    decimal DespesasExtras,
    decimal DespesasAdicionais,
    decimal Balanco,
    string? Observacao
);

public record UpdateSaldoObservacaoDto(string? Observacao);
