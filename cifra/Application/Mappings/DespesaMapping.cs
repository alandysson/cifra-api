using controleDeGastos.Domain.Entities;
using controleDeGastos.Application.DTOs;

namespace controleDeGastos.Application.Mappings;

public static class DespesaMapping
{
    public static DespesaDto ToDto(this Despesa despesa, decimal totalReceita)
        => new(
            despesa.DespesaId,
            despesa.MesId,
            despesa.TipoDespesaId,
            despesa.TipoDespesa.Nome,
            despesa.SubCategoriaId,
            despesa.SubCategoria.Nome,
            despesa.SubCategoria.Categoria.Nome,
            despesa.Descricao,
            despesa.Valor,
            despesa.DataDespesa,
            totalReceita > 0 ? Math.Round((despesa.Valor / totalReceita) * 100, 2) : null
        );
}
