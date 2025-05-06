namespace SistemaCitasMedicas.Application.Dtos.Doctor;

public class DoctorCreateDto
{
    public string Nombre { get; set; } = null!;
    public string Especialidad { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int CentroMedicoId { get; set; }
}