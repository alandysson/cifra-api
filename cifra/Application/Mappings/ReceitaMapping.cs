using controleDeGastos.Domain.Entities;
using controleDeGastos.Application.DTOs;

namespace controleDeGastos.Application.Mappings;

public static class ReceitaMapping
{
    public static ReceitaDto ToDto(this Receita receita)
        => new(receita.ReceitaId, receita.MesId, receita.Descricao, receita.Valor);
}
