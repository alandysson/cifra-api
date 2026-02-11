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
public class ReceitasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ReceitasController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<ReceitaDto>>> GetByMes(int mesId, int page = 1, int pageSize = 10)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound("Mês não encontrado.");

        var receitas = await _unitOfWork.Receitas.GetByMesIdAsync(mesId);
        var result = receitas.Select(r => r.ToDto());
        return Ok(PagedResponseDto<ReceitaDto>.Create(result, page, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReceitaDto>> GetById(int mesId, int id)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound();

        var receita = await _unitOfWork.Receitas.GetByIdAsync(id);
        if (receita == null || receita.MesId != mesId)
            return NotFound();

        return Ok(receita.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<ReceitaDto>> Create(int mesId, CreateReceitaDto dto)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound("Mês não encontrado.");

        var receita = new Receita
        {
            MesId = mesId,
            Descricao = dto.Descricao,
            Valor = dto.Valor
        };

        await _unitOfWork.Receitas.AddAsync(receita);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { mesId, id = receita.ReceitaId }, receita.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int mesId, int id, UpdateReceitaDto dto)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound();

        var receita = await _unitOfWork.Receitas.GetByIdAsync(id);
        if (receita == null || receita.MesId != mesId)
            return NotFound();

        receita.Descricao = dto.Descricao;
        receita.Valor = dto.Valor;

        await _unitOfWork.Receitas.UpdateAsync(receita);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int mesId, int id)
    {
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.Ano.UsuarioId != User.GetUsuarioId())
            return NotFound();

        var receita = await _unitOfWork.Receitas.GetByIdAsync(id);
        if (receita == null || receita.MesId != mesId)
            return NotFound();

        await _unitOfWork.Receitas.DeleteAsync(receita);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
