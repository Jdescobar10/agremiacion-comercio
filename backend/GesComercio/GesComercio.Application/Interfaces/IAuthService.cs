using GesComercio.Application.DTOs;

namespace GesComercio.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
}