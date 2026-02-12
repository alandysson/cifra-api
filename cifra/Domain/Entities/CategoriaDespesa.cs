namespace controleDeGastos.Domain.Entities;

public class CategoriaDespesa
{
    public int CategoriaId { get; set; }
    public string Nome { get; set; } = string.Empty;

    // Navegação
    public ICollection<SubCategoriaDespesa> SubCategorias { get; set; } = new List<SubCategoriaDespesa>();
}
