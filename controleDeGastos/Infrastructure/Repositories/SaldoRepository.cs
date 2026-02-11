using Microsoft.EntityFrameworkCore;
using controleDeGastos.Domain.Entities;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Infrastructure.Data;

namespace controleDeGastos.Infrastructure.Repositories;

public class SaldoRepository : Repository<Saldo>, ISaldoRepository
{
    public SaldoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Saldo?> GetByMesIdAsync(int mesId)
    {
        return await _dbSet.FirstOrDefaultAsync(s => s.MesId == mesId);
    }
}
