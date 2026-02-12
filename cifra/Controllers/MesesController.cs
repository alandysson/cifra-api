using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Application.DTOs;
using controleDeGastos.Application.Mappings;
using controleDeGastos.Application.Extensions;

namespace controleDeGastos.Controllers;

[ApiController]
[Route("api/anos/{anoId}/[controller]")]
[Authorize]
public class MesesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public MesesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<MesDto>>> GetByAno(int anoId, int page = 1, int pageSize = 12)
    {
        var usuarioId = User.GetUsuarioId();
        var ano = await _unitOfWork.Anos.GetByIdAsync(anoId);
        if (ano == null || ano.UsuarioId != usuarioId)
            return NotFound("Ano não encontrado.");

        var meses = await _unitOfWork.Meses.GetByAnoIdAsync(anoId);
        var result = meses.Select(m => m.ToDto());
        return Ok(PagedResponseDto<MesDto>.Create(result, page, pageSize));
    }

    [HttpGet("{mesId}")]
    public async Task<ActionResult<MesDto>> GetById(int anoId, int mesId)
    {
        var usuarioId = User.GetUsuarioId();
        var mes = await _unitOfWork.Meses.GetWithAnoAsync(mesId);
        if (mes == null || mes.AnoId != anoId || mes.Ano.UsuarioId != usuarioId)
            return NotFound();

        return Ok(mes.ToDto());
    }
}
