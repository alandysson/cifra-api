using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using controleDeGastos.Domain.Entities;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Application.DTOs;
using controleDeGastos.Application.Mappings;
using controleDeGastos.Application.Extensions;

namespace controleDeGastos.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AnosController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AnosController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<AnoDto>>> GetAll(int page = 1, int pageSize = 10)
    {
        var usuarioId = User.GetUsuarioId();
        var anos = await _unitOfWork.Anos.GetByUsuarioIdAsync(usuarioId);
        var result = anos.Select(a => a.ToDto()).OrderByDescending(a => a.Numero);
        return Ok(PagedResponseDto<AnoDto>.Create(result, page, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AnoComMesesDto>> GetById(int id)
    {
        var usuarioId = User.GetUsuarioId();
        var ano = await _unitOfWork.Anos.GetWithMesesAsync(id);
        if (ano == null || ano.UsuarioId != usuarioId)
            return NotFound();

        return Ok(ano.ToComMesesDto());
    }

    [HttpPost]
    public async Task<ActionResult<AnoDto>> Create(CreateAnoDto dto)
    {
        var usuarioId = User.GetUsuarioId();
        var existente = await _unitOfWork.Anos.GetByNumeroAndUsuarioIdAsync(dto.Numero, usuarioId);
        if (existente != null)
            return BadRequest($"Ano {dto.Numero} já existe.");

        var nomesMeses = new[]
        {
            "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
            "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"
        };

        var ano = new Ano { Numero = dto.Numero, UsuarioId = usuarioId };

        for (int i = 0; i < 12; i++)
        {
            ano.Meses.Add(new Mes { Nome = nomesMeses[i], Numero = i + 1 });
        }

        await _unitOfWork.Anos.AddAsync(ano);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = ano.AnoId }, ano.ToDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var usuarioId = User.GetUsuarioId();
        var ano = await _unitOfWork.Anos.GetByIdAsync(id);
        if (ano == null || ano.UsuarioId != usuarioId)
            return NotFound();

        await _unitOfWork.Anos.DeleteAsync(ano);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
