namespace GesComercio.Application.DTOs;

public class ComercianteDto
{
    public int Id { get; set; }
    public string RazonSocial { get; set; } = string.Empty;
    public int MunicipioId { get; set; }
    public string Municipio { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? CorreoElectronico { get; set; }
    public DateTime FechaRegistro { get; set; }
    public bool Estado { get; set; }
}
