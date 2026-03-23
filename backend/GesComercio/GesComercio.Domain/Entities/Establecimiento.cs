namespace GesComercio.Domain.Entities;

public class Establecimiento
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Ingresos { get; set; }
    public int NumeroEmpleados { get; set; }
    public int ComercianteId { get; set; }
    public DateTime ActualizadoEn { get; set; }
    public string? ActualizadoPor { get; set; }

    // Navegación
    public Comerciante Comerciante { get; set; } = null!;
}
