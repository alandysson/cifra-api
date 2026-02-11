using controleDeGastos.Domain.Entities;
using controleDeGastos.Application.DTOs;

namespace controleDeGastos.Application.Mappings;

public static class CategoriaMapping
{
    public static CategoriaDto ToDto(this CategoriaDespesa categoria)
        => new(
            categoria.CategoriaId,
            categoria.Nome,
            categoria.SubCategorias.Select(s => s.ToDto()).ToList()
        );

    public static SubCategoriaDto ToDto(this SubCategoriaDespesa subCategoria)
        => new(subCategoria.SubCategoriaId, subCategoria.Nome);

    public static SubCategoriaDetalhadaDto ToDetalhadaDto(this SubCategoriaDespesa subCategoria)
        => new(subCategoria.SubCategoriaId, subCategoria.CategoriaId, subCategoria.Nome, subCategoria.Categoria.Nome);

    public static TipoDespesaDto ToDto(this TipoDespesa tipoDespesa)
        => new(tipoDespesa.TipoDespesaId, tipoDespesa.Nome);
}
