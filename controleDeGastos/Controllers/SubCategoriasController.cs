using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using controleDeGastos.Domain.Entities;
using controleDeGastos.Domain.Interfaces;
using controleDeGastos.Application.DTOs;
using controleDeGastos.Application.Mappings;

namespace controleDeGastos.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubCategoriasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public SubCategoriasController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<SubCategoriaDetalhadaDto>>> GetAll(int page = 1, int pageSize = 10)
    {
        var subCategorias = await _unitOfWork.SubCategorias.GetAllAsync();
        var result = new List<SubCategoriaDetalhadaDto>();

        foreach (var s in subCategorias)
        {
            var sub = await _unitOfWork.SubCategorias.GetByIdWithCategoriaAsync(s.SubCategoriaId);
            if (sub != null)
                result.Add(sub.ToDetalhadaDto());
        }

        var ordered = result.OrderBy(s => s.CategoriaNome).ThenBy(s => s.Nome);
        return Ok(PagedResponseDto<SubCategoriaDetalhadaDto>.Create(ordered, page, pageSize));
    }

    [HttpGet("por-categoria/{categoriaId}")]
    public async Task<ActionResult<PagedResponseDto<SubCategoriaDetalhadaDto>>> GetByCategoria(int categoriaId, int page = 1, int pageSize = 10)
    {
        var categoria = await _unitOfWork.Categorias.GetByIdAsync(categoriaId);
        if (categoria == null)
            return NotFound("Categoria não encontrada.");

        var subCategorias = await _unitOfWork.SubCategorias.GetByCategoriaIdAsync(categoriaId);
        var result = subCategorias.Select(s => s.ToDetalhadaDto());
        return Ok(PagedResponseDto<SubCategoriaDetalhadaDto>.Create(result, page, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubCategoriaDetalhadaDto>> GetById(int id)
    {
        var subCategoria = await _unitOfWork.SubCategorias.GetByIdWithCategoriaAsync(id);
        if (subCategoria == null)
            return NotFound();

        return Ok(subCategoria.ToDetalhadaDto());
    }

    [HttpPost]
    public async Task<ActionResult<SubCategoriaDetalhadaDto>> Create(CreateSubCategoriaDto dto)
    {
        var categoria = await _unitOfWork.Categorias.GetByIdAsync(dto.CategoriaId);

        if (categoria == null)
            return NotFound("Categoria não encontrada.");

        var existente = await _unitOfWork.SubCategorias.GetByNomeAndCategoriaIdAsync(dto.Nome, dto.CategoriaId);
        if (existente != null)
            return BadRequest("Já existe uma subcategoria com esse nome nesta categoria.");

        var subCategoria = new SubCategoriaDespesa
        {
            CategoriaId = dto.CategoriaId,
            Nome = dto.Nome
        };

        await _unitOfWork.SubCategorias.AddAsync(subCategoria);
        await _unitOfWork.SaveChangesAsync();

        var criada = await _unitOfWork.SubCategorias.GetByIdWithCategoriaAsync(subCategoria.SubCategoriaId);

        return CreatedAtAction(nameof(GetById), new { id = subCategoria.SubCategoriaId },
            criada!.ToDetalhadaDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateSubCategoriaDto dto)
    {
        var subCategoria = await _unitOfWork.SubCategorias.GetByIdAsync(id);
        if (subCategoria == null)
            return NotFound();

        var categoria = await _unitOfWork.Categorias.GetByIdAsync(dto.CategoriaId);
        if (categoria == null)
            return NotFound("Categoria não encontrada.");

        var existente = await _unitOfWork.SubCategorias.GetByNomeAndCategoriaIdAsync(dto.Nome, dto.CategoriaId);
        if (existente != null && existente.SubCategoriaId != id)
            return BadRequest("Já existe uma subcategoria com esse nome nesta categoria.");

        subCategoria.CategoriaId = dto.CategoriaId;
        subCategoria.Nome = dto.Nome;

        await _unitOfWork.SubCategorias.UpdateAsync(subCategoria);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var subCategoria = await _unitOfWork.SubCategorias.GetByIdAsync(id);
        if (subCategoria == null)
            return NotFound();

        await _unitOfWork.SubCategorias.DeleteAsync(subCategoria);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
