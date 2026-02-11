namespace controleDeGastos.Application.DTOs;

public record AnoDto(int AnoId, int Numero);

public record CreateAnoDto(int Numero);

public record AnoComMesesDto(int AnoId, int Numero, List<MesResumoDto> Meses);
