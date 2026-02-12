namespace controleDeGastos.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUsuarioRepository Usuarios { get; }
    IAnoRepository Anos { get; }
    IMesRepository Meses { get; }
    IReceitaRepository Receitas { get; }
    IInvestimentoRepository Investimentos { get; }
    IDespesaRepository Despesas { get; }
    ISaldoRepository Saldos { get; }
    ICategoriaRepository Categorias { get; }
    ISubCategoriaRepository SubCategorias { get; }
    IDespesaRecorrenteRepository DespesasRecorrentes { get; }

    Task<int> SaveChangesAsync();
}
