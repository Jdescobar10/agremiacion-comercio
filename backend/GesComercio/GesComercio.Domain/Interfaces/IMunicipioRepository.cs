using GesComercio.Domain.Entities;

namespace GesComercio.Domain.Interfaces;

public interface IMunicipioRepository
{
    Task<IEnumerable<Municipio>> GetAllAsync();
}