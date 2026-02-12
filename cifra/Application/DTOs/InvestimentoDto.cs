namespace controleDeGastos.Application.DTOs;

public record InvestimentoDto(
    int InvestimentoId, 
    int MesId, 
    string Descricao, 
    decimal Valor, 
    decimal? PercentualReceita
);

public record CreateInvestimentoDto(string Descricao, decimal Valor);

public record UpdateInvestimentoDto(string Descricao, decimal Valor);
