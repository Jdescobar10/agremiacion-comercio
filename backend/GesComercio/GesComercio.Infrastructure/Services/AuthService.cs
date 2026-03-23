using GesComercio.Application.DTOs;
using GesComercio.Application.Interfaces;
using GesComercio.Domain.Interfaces;
using BCrypt.Net;

namespace GesComercio.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUsuarioRepository usuarioRepository,
        ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
    {
        // 1. Buscar usuario por correo
        var usuario = await _usuarioRepository.GetByCorreoAsync(request.CorreoElectronico);
        if (usuario == null) return null;

        // 2. Verificar contraseña con BCrypt
        var passwordValido = BCrypt.Net.BCrypt.Verify(request.Contrasena, usuario.Contrasena);
        if (!passwordValido) return null;

        // 3. Generar token
        var token = _tokenService.GenerarToken(usuario);

        return new LoginResponseDto
        {
            Token = token,
            Nombre = usuario.Nombre,
            Rol = usuario.Rol,
            Expiracion = DateTime.UtcNow.AddHours(1)
        };
    }
}
