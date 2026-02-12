using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using controleDeGastos.Domain.Entities;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Application.DTOs;
using controleDeGastos.Application.Mappings;
using controleDeGastos.Application.Extensions;

namespace controleDeGastos.Controllers;

[ApiController]
[Route("api/meses/{mesId}/[controller]")]
[Authorize]
public class InvestimentosController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public InvestimentosController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<InvestimentoDto>>> GetByMes(int mesId, int page = 1, int pageSize = 10)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound("Mês não encontrado.");

        var investimentos = await _unitOfWork.Investimentos.GetByMesIdAsync(mesId);
        var totalReceita = await _unitOfWork.Receitas.GetTotalByMesIdAsync(mesId);

        var result = investimentos.Select(i => i.ToDto(totalReceita));
        return Ok(PagedResponseDto<InvestimentoDto>.Create(result, page, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InvestimentoDto>> GetById(int mesId, int id)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound();

        var investimento = await _unitOfWork.Investimentos.GetByIdAsync(id);
        if (investimento == null || investimento.MesId != mesId)
            return NotFound();

        var totalReceita = await _unitOfWork.Receitas.GetTotalByMesIdAsync(mesId);
        return Ok(investimento.ToDto(totalReceita));
    }

    [HttpPost]
    public async Task<ActionResult<InvestimentoDto>> Create(int mesId, CreateInvestimentoDto dto)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound("Mês não encontrado.");

        var investimento = new Investimento
        {
            MesId = mesId,
            Descricao = dto.Descricao,
            Valor = dto.Valor
        };

        await _unitOfWork.Investimentos.AddAsync(investimento);
        await _unitOfWork.SaveChangesAsync();

        var totalReceita = await _unitOfWork.Receitas.GetTotalByMesIdAsync(mesId);
        return CreatedAtAction(nameof(GetById), new { mesId, id = investimento.InvestimentoId },
            investimento.ToDto(totalReceita));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int mesId, int id, UpdateInvestimentoDto dto)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound();

        var investimento = await _unitOfWork.Investimentos.GetByIdAsync(id);
        if (investimento == null || investimento.MesId != mesId)
            return NotFound();

        investimento.Descricao = dto.Descricao;
        investimento.Valor = dto.Valor;

        await _unitOfWork.Investimentos.UpdateAsync(investimento);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int mesId, int id)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound();

        var investimento = await _unitOfWork.Investimentos.GetByIdAsync(id);
        if (investimento == null || investimento.MesId != mesId)
            return NotFound();

        await _unitOfWork.Investimentos.DeleteAsync(investimento);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
