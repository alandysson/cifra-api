namespace controleDeGastos.Domain.Entities;

public class Ano
{
    public int AnoId { get; set; }
    public int Numero { get; set; }
    public int UsuarioId { get; set; }

    // Navegação
    public Usuario Usuario { get; set; } = null!;
    public ICollection<Mes> Meses { get; set; } = new List<Mes>();
}
