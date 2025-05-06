using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaCitasMedicas.Application.Dtos.Doctor;
using SistemaCitasMedicas.Application.Interfaces;
using SistemaCitasMedicas.Domain.Entities;
using SistemaCitasMedicas.Infrastructure.Data;

namespace SistemaCitasMedicas.Application.Services;

public class DoctorService : IDoctorService
{
    private readonly SistemaCitasMedicasDbContext _context;
    private readonly IMapper _mapper;

    public DoctorService(SistemaCitasMedicasDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DoctorReadDto>> GetAllAsync()
    {
        var doctores = await _context.Doctores
            .Include(d => d.CentroMedico)
            .ToListAsync();

        return _mapper.Map<List<DoctorReadDto>>(doctores);
    }

    public async Task<DoctorReadDto?> GetByIdAsync(int id)
    {
        var doctor = await _context.Doctores
            .Include(d => d.CentroMedico)
            .FirstOrDefaultAsync(d => d.Id == id);

        return doctor == null ? null : _mapper.Map<DoctorReadDto>(doctor);
    }

    public async Task<DoctorReadDto> CreateAsync(DoctorCreateDto dto)
    {
        var doctor = _mapper.Map<Doctor>(dto);
        _context.Doctores.Add(doctor);
        await _context.SaveChangesAsync();
        await _context.Entry(doctor).Reference(d => d.CentroMedico).LoadAsync();
        return _mapper.Map<DoctorReadDto>(doctor);
    }

    public async Task<bool> UpdateAsync(int id, DoctorUpdateDto dto)
    {
        var doctor = await _context.Doctores.FindAsync(id);
        if (doctor == null) return false;

        _mapper.Map(dto, doctor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var doctor = await _context.Doctores.FindAsync(id);
        if (doctor == null) return false;

        _context.Doctores.Remove(doctor);
        await _context.SaveChangesAsync();
        return true;
    }
}
