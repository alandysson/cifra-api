using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using controleDeGastos.Domain.Entities;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Application.DTOs;
using controleDeGastos.Application.Mappings;
using controleDeGastos.Application.Extensions;

namespace controleDeGastos.Controllers;

[ApiController]
[Route("api/despesas-recorrentes")]
[Authorize]
public class DespesasRecorrentesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public DespesasRecorrentesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<DespesaRecorrenteDto>>> GetAll(int page = 1, int pageSize = 10, string? descricao = null)
    {
        var usuarioId = User.GetUsuarioId();
        var recorrentes = await _unitOfWork.DespesasRecorrentes.GetByUsuarioIdAsync(usuarioId);
        var result = recorrentes.Select(r => r.ToDto());

        if (!string.IsNullOrWhiteSpace(descricao))
            result = result.Where(r => r.Descricao.Contains(descricao, StringComparison.OrdinalIgnoreCase));

        return Ok(PagedResponseDto<DespesaRecorrenteDto>.Create(result, page, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DespesaRecorrenteDto>> GetById(int id)
    {
        var recorrente = await _unitOfWork.DespesasRecorrentes.GetByIdWithDespesasAsync(id);
        if (recorrente == null || recorrente.UsuarioId != User.GetUsuarioId())
            return NotFound();

        return Ok(recorrente.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<DespesaRecorrenteDto>> Create(CreateDespesaRecorrenteDto dto)
    {
        var usuarioId = User.GetUsuarioId();

        // Validar que intervalo é válido
        if (dto.AnoInicio > dto.AnoFim || (dto.AnoInicio == dto.AnoFim && dto.MesInicio > dto.MesFim))
            return BadRequest("O período inicial deve ser anterior ou igual ao período final.");

        if (dto.MesInicio < 1 || dto.MesInicio > 12 || dto.MesFim < 1 || dto.MesFim > 12)
            return BadRequest("Mês deve estar entre 1 e 12.");

        // Buscar meses do usuário no intervalo
        var meses = await _unitOfWork.Meses.GetByUsuarioIdAndRangeAsync(
            usuarioId, dto.AnoInicio, dto.MesInicio, dto.AnoFim, dto.MesFim);

        var listaMeses = meses.ToList();
        if (listaMeses.Count == 0)
            return BadRequest("Nenhum mês encontrado no intervalo informado. Verifique se os anos e meses já foram criados.");

        // Criar template
        var recorrente = new DespesaRecorrente
        {
            UsuarioId = usuarioId,
            TipoDespesaId = dto.TipoDespesaId,
            SubCategoriaId = dto.SubCategoriaId,
            Descricao = dto.Descricao,
            Valor = dto.Valor,
            AnoInicio = dto.AnoInicio,
            MesInicio = dto.MesInicio,
            AnoFim = dto.AnoFim,
            MesFim = dto.MesFim
        };

        await _unitOfWork.DespesasRecorrentes.AddAsync(recorrente);
        await _unitOfWork.SaveChangesAsync();

        // Criar despesa individual por mês
        foreach (var mes in listaMeses)
        {
            var despesa = new Despesa
            {
                MesId = mes.MesId,
                TipoDespesaId = dto.TipoDespesaId,
                SubCategoriaId = dto.SubCategoriaId,
                Descricao = dto.Descricao,
                Valor = dto.Valor,
                DespesaRecorrenteId = recorrente.DespesaRecorrenteId
            };
            await _unitOfWork.Despesas.AddAsync(despesa);
        }

        await _unitOfWork.SaveChangesAsync();

        // Recarregar com includes
        var criada = await _unitOfWork.DespesasRecorrentes.GetByIdWithDespesasAsync(recorrente.DespesaRecorrenteId);

        return CreatedAtAction(nameof(GetById), new { id = recorrente.DespesaRecorrenteId }, criada!.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDespesaRecorrenteDto dto)
    {
        var recorrente = await _unitOfWork.DespesasRecorrentes.GetByIdWithDespesasAsync(id);
        if (recorrente == null || recorrente.UsuarioId != User.GetUsuarioId())
            return NotFound();

        // Atualizar template
        recorrente.TipoDespesaId = dto.TipoDespesaId;
        recorrente.SubCategoriaId = dto.SubCategoriaId;
        recorrente.Descricao = dto.Descricao;
        recorrente.Valor = dto.Valor;
        await _unitOfWork.DespesasRecorrentes.UpdateAsync(recorrente);

        // Atualizar todas as despesas vinculadas
        foreach (var despesa in recorrente.Despesas)
        {
            despesa.TipoDespesaId = dto.TipoDespesaId;
            despesa.SubCategoriaId = dto.SubCategoriaId;
            despesa.Descricao = dto.Descricao;
            despesa.Valor = dto.Valor;
            await _unitOfWork.Despesas.UpdateAsync(despesa);
        }

        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var recorrente = await _unitOfWork.DespesasRecorrentes.GetByIdWithDespesasAsync(id);
        if (recorrente == null || recorrente.UsuarioId != User.GetUsuarioId())
            return NotFound();

        // Excluir todas as despesas vinculadas
        foreach (var despesa in recorrente.Despesas.ToList())
        {
            await _unitOfWork.Despesas.DeleteAsync(despesa);
        }

        // Excluir template
        await _unitOfWork.DespesasRecorrentes.DeleteAsync(recorrente);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
