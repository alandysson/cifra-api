using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Domain.Enums;
using controleDeGastos.Application.DTOs;
using controleDeGastos.Application.Mappings;
using controleDeGastos.Application.Extensions;

namespace controleDeGastos.Controllers;

[ApiController]
[Route("api/meses/{mesId}/[controller]")]
[Authorize]
public class ResumoController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ResumoController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<ResumoMensalDto>> GetResumoMensal(int mesId)
    {
        var mes = await _unitOfWork.Meses.GetWithDetalhesAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound("Mês não encontrado.");

        // Totais
        var totalReceitas = mes.Receitas.Sum(r => r.Valor);
        var totalInvestimentos = mes.Investimentos.Sum(i => i.Valor);

        var despesasFixas = mes.Despesas.Where(d => d.TipoDespesaId == (int)ETipoDespesa.Fixa).Sum(d => d.Valor);
        var despesasVariaveis = mes.Despesas.Where(d => d.TipoDespesaId == (int)ETipoDespesa.Variavel).Sum(d => d.Valor);
        var despesasExtras = mes.Despesas.Where(d => d.TipoDespesaId == (int)ETipoDespesa.Extra).Sum(d => d.Valor);
        var despesasAdicionais = mes.Despesas.Where(d => d.TipoDespesaId == (int)ETipoDespesa.Adicional).Sum(d => d.Valor);
        var totalDespesas = despesasFixas + despesasVariaveis + despesasExtras + despesasAdicionais;

        var balanco = totalReceitas - (totalInvestimentos + totalDespesas);

        // Mapear DTOs
        var receitasDto = mes.Receitas.Select(r => r.ToDto()).ToList();
        var investimentosDto = mes.Investimentos.Select(i => i.ToDto(totalReceitas)).ToList();
        var despesasDto = mes.Despesas.Select(d => d.ToDto(totalReceitas)).ToList();

        var despesasAgrupadas = new DespesasAgrupadasDto(
            despesasFixas,
            despesasVariaveis,
            despesasExtras,
            despesasAdicionais,
            totalDespesas,
            despesasDto
        );

        return Ok(new ResumoMensalDto(
            mes.MesId,
            mes.Nome,
            mes.Ano.Numero,
            totalReceitas,
            totalInvestimentos,
            despesasAgrupadas,
            balanco,
            receitasDto,
            investimentosDto
        ));
    }
}
