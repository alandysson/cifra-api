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
public class DespesasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public DespesasController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<DespesaDto>>> GetByMes(int mesId, int page = 1, int pageSize = 10, string? descricao = null, string? categoria = null)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound("Mês não encontrado.");

        var despesas = await _unitOfWork.Despesas.GetByMesIdAsync(mesId);
        var totalReceita = await _unitOfWork.Receitas.GetTotalByMesIdAsync(mesId);

        var result = despesas.Select(d => d.ToDto(totalReceita));

        if (!string.IsNullOrWhiteSpace(descricao))
            result = result.Where(d => d.Descricao.Contains(descricao, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(categoria))
        {
            result = result.Where(d => d.TipoDespesaNome.Contains(categoria, StringComparison.OrdinalIgnoreCase));
        }
        
        return Ok(PagedResponseDto<DespesaDto>.Create(result, page, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DespesaDto>> GetById(int mesId, int id)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound();

        var despesas = await _unitOfWork.Despesas.GetByMesIdAsync(mesId);
        var despesa = despesas.FirstOrDefault(d => d.DespesaId == id);

        if (despesa == null)
            return NotFound();

        var totalReceita = await _unitOfWork.Receitas.GetTotalByMesIdAsync(mesId);
        return Ok(despesa.ToDto(totalReceita));
    }

    [HttpPost]
    public async Task<ActionResult<DespesaDto>> Create(int mesId, CreateDespesaDto dto)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound("Mês não encontrado.");

        var despesa = new Despesa
        {
            MesId = mesId,
            TipoDespesaId = dto.TipoDespesaId,
            SubCategoriaId = dto.SubCategoriaId,
            Descricao = dto.Descricao,
            Valor = dto.Valor
        };

        await _unitOfWork.Despesas.AddAsync(despesa);
        await _unitOfWork.SaveChangesAsync();

        // Recarregar com includes
        var despesas = await _unitOfWork.Despesas.GetByMesIdAsync(mesId);
        var despesaCriada = despesas.First(d => d.DespesaId == despesa.DespesaId);

        var totalReceita = await _unitOfWork.Receitas.GetTotalByMesIdAsync(mesId);

        return CreatedAtAction(nameof(GetById), new { mesId, id = despesa.DespesaId },
            despesaCriada.ToDto(totalReceita));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int mesId, int id, UpdateDespesaDto dto)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound();

        var despesa = await _unitOfWork.Despesas.GetByIdAsync(id);
        if (despesa == null || despesa.MesId != mesId)
            return NotFound();

        despesa.TipoDespesaId = dto.TipoDespesaId;
        despesa.SubCategoriaId = dto.SubCategoriaId;
        despesa.Descricao = dto.Descricao;
        despesa.Valor = dto.Valor;

        await _unitOfWork.Despesas.UpdateAsync(despesa);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int mesId, int id)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound();

        var despesa = await _unitOfWork.Despesas.GetByIdAsync(id);
        if (despesa == null || despesa.MesId != mesId)
            return NotFound();

        await _unitOfWork.Despesas.DeleteAsync(despesa);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
