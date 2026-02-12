namespace controleDeGastos.Application.DTOs;

public record DespesaRecorrenteDto(
    int DespesaRecorrenteId,
    int TipoDespesaId,
    string TipoDespesaNome,
    int SubCategoriaId,
    string SubCategoriaNome,
    string CategoriaNome,
    string Descricao,
    decimal Valor,
    int AnoInicio,
    int MesInicio,
    int AnoFim,
    int MesFim,
    List<DespesaRecorrenteItemDto> Despesas
);

public record DespesaRecorrenteItemDto(
    int DespesaId,
    string MesNome,
    int AnoNumero,
    decimal Valor
);

public record CreateDespesaRecorrenteDto(
    int TipoDespesaId,
    int SubCategoriaId,
    string Descricao,
    decimal Valor,
    int AnoInicio,
    int MesInicio,
    int AnoFim,
    int MesFim
);

public record UpdateDespesaRecorrenteDto(
    int TipoDespesaId,
    int SubCategoriaId,
    string Descricao,
    decimal Valor
);
