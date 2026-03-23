namespace GesComercio.Domain.Entities;

public class Comerciante
{
    public int Id { get; set; }
    public string RazonSocial { get; set; } = string.Empty;
    public int MunicipioId { get; set; }
    public string? Telefono { get; set; }
    public string? CorreoElectronico { get; set; }
    public DateTime FechaRegistro { get; set; }
    public bool Estado { get; set; }
    public DateTime ActualizadoEn { get; set; }
    public string? ActualizadoPor { get; set; }

    // Navegación
    public Municipio Municipio { get; set; } = null!;
    public ICollection<Establecimiento> Establecimientos { get; set; }
        = new List<Establecimiento>();
}
