namespace controleDeGastos.Domain.Entities;

public class SubCategoriaDespesa
{
    public int SubCategoriaId { get; set; }
    public int CategoriaId { get; set; }
    public string Nome { get; set; } = string.Empty;

    // Navegação
    public CategoriaDespesa Categoria { get; set; } = null!;
    public ICollection<Despesa> Despesas { get; set; } = new List<Despesa>();
}
