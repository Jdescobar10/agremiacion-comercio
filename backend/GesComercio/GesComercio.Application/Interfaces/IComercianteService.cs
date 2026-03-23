using GesComercio.Application.DTOs;

namespace GesComercio.Application.Interfaces;

public interface IComercianteService
{
    Task<PagedResponseDto<ComercianteDto>> GetAllAsync(int page, int pageSize, string? filtro, bool? estado);
    Task<ComercianteDto?> GetByIdAsync(int id);
    Task<ComercianteDto> CreateAsync(ComercianteRequestDto request, string usuarioActual);
    Task<ComercianteDto> UpdateAsync(int id, ComercianteRequestDto request, string usuarioActual);
    Task<bool> DeleteAsync(int id);
    Task<bool> PatchEstadoAsync(int id, bool estado);
}