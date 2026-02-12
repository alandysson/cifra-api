namespace controleDeGastos.Domain.Entities;

public class DespesaRecorrente
{
    public int DespesaRecorrenteId { get; set; }
    public int UsuarioId { get; set; }
    public int TipoDespesaId { get; set; }
    public int SubCategoriaId { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public int AnoInicio { get; set; }
    public int MesInicio { get; set; }
    public int AnoFim { get; set; }
    public int MesFim { get; set; }

    // Navegação
    public Usuario Usuario { get; set; } = null!;
    public TipoDespesa TipoDespesa { get; set; } = null!;
    public SubCategoriaDespesa SubCategoria { get; set; } = null!;
    public ICollection<Despesa> Despesas { get; set; } = new List<Despesa>();
}
