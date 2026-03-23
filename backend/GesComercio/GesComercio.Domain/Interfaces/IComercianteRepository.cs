using GesComercio.Domain.Entities;

namespace GesComercio.Domain.Interfaces;

public interface IComercianteRepository
{
    Task<IEnumerable<Comerciante>> GetAllAsync(int page, int pageSize, string? filtro, bool? estado);
    Task<int> GetTotalAsync(string? filtro, bool? estado);
    Task<Comerciante?> GetByIdAsync(int id);
    Task<Comerciante> CreateAsync(Comerciante comerciante);
    Task<Comerciante> UpdateAsync(Comerciante comerciante);
    Task<bool> DeleteAsync(int id);
    Task<bool> PatchEstadoAsync(int id, bool estado);
}