namespace controleDeGastos.Application.DTOs;

public record CategoriaDto(int CategoriaId, string Nome, List<SubCategoriaDto> SubCategorias);

public record SubCategoriaDto(int SubCategoriaId, string Nome);

public record SubCategoriaDetalhadaDto(int SubCategoriaId, int CategoriaId, string Nome, string CategoriaNome);

public record CreateCategoriaDto(string Nome);

public record UpdateCategoriaDto(string Nome);

public record CreateSubCategoriaDto(int CategoriaId, string Nome);

public record UpdateSubCategoriaDto(int CategoriaId, string Nome);

public record TipoDespesaDto(int TipoDespesaId, string Nome);
