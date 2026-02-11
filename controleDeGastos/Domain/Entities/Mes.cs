namespace controleDeGastos.Domain.Entities;

public class Mes
{
    public int MesId { get; set; }
    public int AnoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Numero { get; set; }

    // Navegação
    public Ano Ano { get; set; } = null!;
    public ICollection<Receita> Receitas { get; set; } = new List<Receita>();
    public ICollection<Investimento> Investimentos { get; set; } = new List<Investimento>();
    public ICollection<Despesa> Despesas { get; set; } = new List<Despesa>();
    public Saldo? Saldo { get; set; }
}
