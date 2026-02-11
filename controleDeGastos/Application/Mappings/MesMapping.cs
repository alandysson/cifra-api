using controleDeGastos.Domain.Entities;
using controleDeGastos.Application.DTOs;

namespace controleDeGastos.Application.Mappings;

public static class MesMapping
{
    public static MesDto ToDto(this Mes mes)
        => new(mes.MesId, mes.AnoId, mes.Nome, mes.Numero);

    public static MesResumoDto ToResumoDto(this Mes mes)
        => new(mes.MesId, mes.Nome, mes.Numero);
}
