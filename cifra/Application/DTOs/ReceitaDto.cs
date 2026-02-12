namespace controleDeGastos.Application.DTOs;

public record ReceitaDto(int ReceitaId, int MesId, string Descricao, decimal Valor);

public record CreateReceitaDto(string Descricao, decimal Valor);

public record UpdateReceitaDto(string Descricao, decimal Valor);
