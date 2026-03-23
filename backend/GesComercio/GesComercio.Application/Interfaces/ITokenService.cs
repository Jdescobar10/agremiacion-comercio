using GesComercio.Domain.Entities;

namespace GesComercio.Application.Interfaces;

public interface ITokenService
{
    string GenerarToken(Usuario usuario);
}
