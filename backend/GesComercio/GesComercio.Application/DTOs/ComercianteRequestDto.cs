namespace GesComercio.Application.DTOs;

public class ComercianteRequestDto
{
    public string RazonSocial { get; set; } = string.Empty;
    public int MunicipioId { get; set; }
    public string? Telefono { get; set; }
    public string? CorreoElectronico { get; set; }
    public bool Estado { get; set; }
}