using GesComercio.Application.DTOs;

namespace GesComercio.Application.Interfaces;

public interface IMunicipioService
{
    Task<IEnumerable<MunicipioDto>> GetAllAsync();
}