using Microsoft.AspNetCore.Mvc;
using SistemaCitasMedicas.Application.Dtos.Doctor;
using SistemaCitasMedicas.Application.Interfaces;

namespace SistemaCitasMedicas.API.Controllers;

[ApiController]
[Route("api/doctores")]
public class DoctoresController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctoresController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _doctorService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _doctorService.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DoctorCreateDto dto)
    {
        var result = await _doctorService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DoctorUpdateDto dto)
    {
        var updated = await _doctorService.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _doctorService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}