namespace controleDeGastos.Domain.Entities;

public class Saldo
{
    public int SaldoId { get; set; }
    public int MesId { get; set; }
    public decimal Receita { get; set; }
    public decimal Investimentos { get; set; }
    public decimal DespesasFixas { get; set; }
    public decimal DespesasVariaveis { get; set; }
    public decimal DespesasExtras { get; set; }
    public decimal DespesasAdicionais { get; set; }
    public decimal Balanco { get; set; }
    public string? Observacao { get; set; }

    // Navegação
    public Mes Mes { get; set; } = null!;
}
