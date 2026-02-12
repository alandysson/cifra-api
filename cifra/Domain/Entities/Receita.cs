namespace controleDeGastos.Domain.Entities;

public class Receita
{
    public int ReceitaId { get; set; }
    public int MesId { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }

    // Navegação
    public Mes Mes { get; set; } = null!;
}
