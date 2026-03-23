using GesComercio.Domain.Entities;

namespace GesComercio.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByCorreoAsync(string correo);
    Task<Usuario?> GetByIdAsync(int id);
}