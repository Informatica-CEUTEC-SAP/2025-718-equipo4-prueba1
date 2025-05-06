namespace SistemaCitasMedicas.Application.Dtos.Doctor;

public class DoctorReadDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Especialidad { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string CentroMedico { get; set; } = null!;
}