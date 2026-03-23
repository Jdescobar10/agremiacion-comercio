using GesComercio.Application.Common;
using GesComercio.Application.DTOs;
using GesComercio.Application.Interfaces;
using GesComercio.Application.Validators;
using Microsoft.AspNetCore.Mvc;

namespace GesComercio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly LoginValidator _loginValidator;

    public AuthController(
        IAuthService authService,
        LoginValidator loginValidator)
    {
        _authService = authService;
        _loginValidator = loginValidator;
    }

    /// <summary>
    /// Endpoint público — no requiere JWT
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        // 1. Validar request con FluentValidation
        var validacion = await _loginValidator.ValidateAsync(request);
        if (!validacion.IsValid)
        {
            var errores = validacion.Errors
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(ApiResponse<List<string>>.Fail(
                "Datos de entrada inválidos", 400)
            );
        }

        // 2. Intentar login
        var resultado = await _authService.LoginAsync(request);
        if (resultado == null)
        {
            return Unauthorized(ApiResponse<string>.Fail(
                "Credenciales incorrectas", 401));
        }

        // 3. Retornar token
        return Ok(ApiResponse<LoginResponseDto>.Ok(resultado, "Login exitoso"));
    }
}