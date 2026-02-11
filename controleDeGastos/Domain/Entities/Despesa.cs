namespace controleDeGastos.Domain.Entities;

public class Despesa
{
    public int DespesaId { get; set; }
    public int MesId { get; set; }
    public int TipoDespesaId { get; set; }
    public int SubCategoriaId { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public decimal? PercentualReceita { get; set; }
    public int? DespesaRecorrenteId { get; set; }

    // Navegação
    public Mes Mes { get; set; } = null!;
    public TipoDespesa TipoDespesa { get; set; } = null!;
    public SubCategoriaDespesa SubCategoria { get; set; } = null!;
    public DespesaRecorrente? DespesaRecorrente { get; set; }
}
