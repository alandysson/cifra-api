namespace controleDeGastos.Application.DTOs;

public record ResumoMensalDto(
    int MesId,
    string MesNome,
    int AnoNumero,
    decimal TotalReceitas,
    decimal TotalInvestimentos,
    DespesasAgrupadasDto Despesas,
    decimal Balanco,
    List<ReceitaDto> Receitas,
    List<InvestimentoDto> Investimentos
);

public record DespesasAgrupadasDto(
    decimal Fixas,
    decimal Variaveis,
    decimal Extras,
    decimal Adicionais,
    decimal Total,
    List<DespesaDto> Lista
);
