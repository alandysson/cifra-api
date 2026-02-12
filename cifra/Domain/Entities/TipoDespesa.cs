namespace controleDeGastos.Domain.Entities;

public class TipoDespesa
{
    public int TipoDespesaId { get; set; }
    public string Nome { get; set; } = string.Empty;

    // Navegação
    public ICollection<Despesa> Despesas { get; set; } = new List<Despesa>();
}
