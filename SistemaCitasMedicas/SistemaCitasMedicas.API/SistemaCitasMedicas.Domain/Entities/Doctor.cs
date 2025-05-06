namespace SistemaCitasMedicas.Domain.Entities;

public class Doctor
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Especialidad { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int CentroMedicoId { get; set; }

    public CentroMedico? CentroMedico { get; set; }
}