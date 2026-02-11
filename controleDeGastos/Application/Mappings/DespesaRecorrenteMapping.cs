using controleDeGastos.Domain.Entities;
using controleDeGastos.Application.DTOs;

namespace controleDeGastos.Application.Mappings;

public static class DespesaRecorrenteMapping
{
    public static DespesaRecorrenteDto ToDto(this DespesaRecorrente dr)
        => new(
            dr.DespesaRecorrenteId,
            dr.TipoDespesaId,
            dr.TipoDespesa.Nome,
            dr.SubCategoriaId,
            dr.SubCategoria.Nome,
            dr.SubCategoria.Categoria.Nome,
            dr.Descricao,
            dr.Valor,
            dr.AnoInicio,
            dr.MesInicio,
            dr.AnoFim,
            dr.MesFim,
            dr.Despesas.Select(d => d.ToRecorrenteItemDto()).ToList()
        );

    public static DespesaRecorrenteItemDto ToRecorrenteItemDto(this Despesa d)
        => new(
            d.DespesaId,
            d.Mes.Nome,
            d.Mes.Ano.Numero,
            d.Valor
        );
}
