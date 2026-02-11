using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Infrastructure.Data;

namespace controleDeGastos.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    
    private IUsuarioRepository? _usuarios;
    private IAnoRepository? _anos;
    private IMesRepository? _meses;
    private IReceitaRepository? _receitas;
    private IInvestimentoRepository? _investimentos;
    private IDespesaRepository? _despesas;
    private ISaldoRepository? _saldos;
    private ICategoriaRepository? _categorias;
    private ISubCategoriaRepository? _subCategorias;
    private IDespesaRecorrenteRepository? _despesasRecorrentes;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IUsuarioRepository Usuarios => _usuarios ??= new UsuarioRepository(_context);
    public IAnoRepository Anos => _anos ??= new AnoRepository(_context);
    public IMesRepository Meses => _meses ??= new MesRepository(_context);
    public IReceitaRepository Receitas => _receitas ??= new ReceitaRepository(_context);
    public IInvestimentoRepository Investimentos => _investimentos ??= new InvestimentoRepository(_context);
    public IDespesaRepository Despesas => _despesas ??= new DespesaRepository(_context);
    public ISaldoRepository Saldos => _saldos ??= new SaldoRepository(_context);
    public ICategoriaRepository Categorias => _categorias ??= new CategoriaRepository(_context);
    public ISubCategoriaRepository SubCategorias => _subCategorias ??= new SubCategoriaRepository(_context);
    public IDespesaRecorrenteRepository DespesasRecorrentes => _despesasRecorrentes ??= new DespesaRecorrenteRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
