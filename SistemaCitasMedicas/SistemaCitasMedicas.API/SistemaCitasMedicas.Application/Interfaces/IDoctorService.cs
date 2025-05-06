using SistemaCitasMedicas.Application.Dtos.Doctor;

namespace SistemaCitasMedicas.Application.Interfaces;

public interface IDoctorService
{
    Task<List<DoctorReadDto>> GetAllAsync();
    Task<DoctorReadDto?> GetByIdAsync(int id);
    Task<DoctorReadDto> CreateAsync(DoctorCreateDto dto);
    Task<bool> UpdateAsync(int id, DoctorUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}