namespace controleDeGastos.Application.DTOs;

public record MesDto(int MesId, int AnoId, string Nome, int Numero);

public record MesResumoDto(int MesId, string Nome, int Numero);
