namespace controleDeGastos.Domain.Entities;

public class Investimento
{
    public int InvestimentoId { get; set; }
    public int MesId { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public decimal? PercentualReceita { get; set; }

    // Navegação
    public Mes Mes { get; set; } = null!;
}
