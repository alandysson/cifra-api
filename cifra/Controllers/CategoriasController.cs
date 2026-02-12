using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Application.DTOs;
using controleDeGastos.Application.Mappings;
using controleDeGastos.Domain.Entities;
using controleDeGastos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace controleDeGastos.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AppDbContext _context;

    public CategoriasController(IUnitOfWork unitOfWork, AppDbContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<CategoriaDto>>> GetAll(int page = 1, int pageSize = 10)
    {
        var categorias = await _unitOfWork.Categorias.GetAllWithSubCategoriasAsync();
        var result = categorias.Select(c => c.ToDto());
        return Ok(PagedResponseDto<CategoriaDto>.Create(result, page, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaDto>> GetById(int id)
    {
        var categoria = await _unitOfWork.Categorias.GetByIdAsync(id);
        if (categoria == null)
            return NotFound();

        return Ok(categoria.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDto>> Create(CreateCategoriaDto dto)
    {
        var existente = await _unitOfWork.Categorias.GetByNomeAsync(dto.Nome);
        if (existente != null)
            return BadRequest("Já existe uma categoria com esse nome.");

        var categoria = new CategoriaDespesa { Nome = dto.Nome };

        await _unitOfWork.Categorias.AddAsync(categoria);
        await _unitOfWork.SaveChangesAsync();

        var criada = await _unitOfWork.Categorias.GetByIdAsync(categoria.CategoriaId);
        return CreatedAtAction(nameof(GetById), new { id = categoria.CategoriaId }, criada!.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCategoriaDto dto)
    {
        var categoria = await _unitOfWork.Categorias.GetByIdAsync(id);
        if (categoria == null)
            return NotFound();

        var existente = await _unitOfWork.Categorias.GetByNomeAsync(dto.Nome);
        if (existente != null && existente.CategoriaId != id)
            return BadRequest("Já existe uma categoria com esse nome.");

        categoria.Nome = dto.Nome;

        await _unitOfWork.Categorias.UpdateAsync(categoria);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var categoria = await _unitOfWork.Categorias.GetByIdAsync(id);
        if (categoria == null)
            return NotFound();

        await _unitOfWork.Categorias.DeleteAsync(categoria);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("tipos-despesa")]
    public async Task<ActionResult<PagedResponseDto<TipoDespesaDto>>> GetTiposDespesa(int page = 1, int pageSize = 10)
    {
        var tipos = await _context.TiposDespesa.ToListAsync();
        var result = tipos.Select(t => t.ToDto());
        return Ok(PagedResponseDto<TipoDespesaDto>.Create(result, page, pageSize));
    }
}
