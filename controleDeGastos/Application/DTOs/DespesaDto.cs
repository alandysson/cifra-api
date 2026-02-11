namespace controleDeGastos.Application.DTOs;

public record DespesaDto(
    int DespesaId,
    int MesId,
    int TipoDespesaId,
    string TipoDespesaNome,
    int SubCategoriaId,
    string SubCategoriaNome,
    string CategoriaNome,
    string Descricao,
    decimal Valor,
    decimal? PercentualReceita
);

public record CreateDespesaDto(
    int TipoDespesaId,
    int SubCategoriaId,
    string Descricao,
    decimal Valor
);

public record UpdateDespesaDto(
    int TipoDespesaId,
    int SubCategoriaId,
    string Descricao,
    decimal Valor
);
