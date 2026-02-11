using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using controleDeGastos.Domain.Entities;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Domain.Enums;
using controleDeGastos.Application.DTOs;
using controleDeGastos.Application.Mappings;
using controleDeGastos.Application.Extensions;

namespace controleDeGastos.Controllers;

[ApiController]
[Route("api/meses/{mesId}/[controller]")]
[Authorize]
public class SaldoController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public SaldoController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<SaldoDto>> GetByMes(int mesId)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound("Mês não encontrado.");

        // Calcular valores
        var totalReceita = await _unitOfWork.Receitas.GetTotalByMesIdAsync(mesId);
        var totalInvestimentos = await _unitOfWork.Investimentos.GetTotalByMesIdAsync(mesId);
        var despesasFixas = await _unitOfWork.Despesas.GetTotalByMesIdAndTipoAsync(mesId, (int)ETipoDespesa.Fixa);
        var despesasVariaveis = await _unitOfWork.Despesas.GetTotalByMesIdAndTipoAsync(mesId, (int)ETipoDespesa.Variavel);
        var despesasExtras = await _unitOfWork.Despesas.GetTotalByMesIdAndTipoAsync(mesId, (int)ETipoDespesa.Extra);
        var despesasAdicionais = await _unitOfWork.Despesas.GetTotalByMesIdAndTipoAsync(mesId, (int)ETipoDespesa.Adicional);

        var balanco = totalReceita - (totalInvestimentos + despesasFixas + despesasVariaveis + despesasExtras + despesasAdicionais);

        // Buscar ou criar saldo
        var saldo = await _unitOfWork.Saldos.GetByMesIdAsync(mesId);

        if (saldo == null)
        {
            saldo = new Saldo
            {
                MesId = mesId,
                Receita = totalReceita,
                Investimentos = totalInvestimentos,
                DespesasFixas = despesasFixas,
                DespesasVariaveis = despesasVariaveis,
                DespesasExtras = despesasExtras,
                DespesasAdicionais = despesasAdicionais,
                Balanco = balanco
            };
            await _unitOfWork.Saldos.AddAsync(saldo);
            await _unitOfWork.SaveChangesAsync();
        }
        else
        {
            // Atualizar valores calculados
            saldo.Receita = totalReceita;
            saldo.Investimentos = totalInvestimentos;
            saldo.DespesasFixas = despesasFixas;
            saldo.DespesasVariaveis = despesasVariaveis;
            saldo.DespesasExtras = despesasExtras;
            saldo.DespesasAdicionais = despesasAdicionais;
            saldo.Balanco = balanco;
            await _unitOfWork.Saldos.UpdateAsync(saldo);
            await _unitOfWork.SaveChangesAsync();
        }

        return Ok(saldo.ToDto());
    }

    [HttpPut("observacao")]
    public async Task<IActionResult> UpdateObservacao(int mesId, UpdateSaldoObservacaoDto dto)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound();

        var saldo = await _unitOfWork.Saldos.GetByMesIdAsync(mesId);
        if (saldo == null)
            return NotFound("Saldo não encontrado. Acesse GET /api/meses/{mesId}/saldo primeiro.");

        saldo.Observacao = dto.Observacao;
        await _unitOfWork.Saldos.UpdateAsync(saldo);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
