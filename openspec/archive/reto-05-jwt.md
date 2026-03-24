# Reto 05 — Autenticación JWT
**Estado:** ✅ Completado y verificado
**Fecha:** 2026-03
**Rama:** `feature/backend-reto-05-jwt`

---

## Archivos Creados

### Domain
- `Entities/Usuario.cs`
- `Entities/Comerciante.cs`
- `Entities/Establecimiento.cs`
- `Entities/Municipio.cs`
- `Enums/Rol.cs`
- `Interfaces/IUsuarioRepository.cs`
- `Interfaces/IComercianteRepository.cs`
- `Interfaces/IMunicipioRepository.cs`

### Application
- `DTOs/LoginRequestDto.cs`
- `DTOs/LoginResponseDto.cs`
- `DTOs/MunicipioDto.cs`
- `DTOs/ComercianteDto.cs`
- `DTOs/ComercianteRequestDto.cs`
- `DTOs/PagedResponseDto.cs`
- `Interfaces/IAuthService.cs`
- `Interfaces/IComercianteService.cs`
- `Interfaces/IMunicipioService.cs`
- `Interfaces/ITokenService.cs`
- `Common/ApiResponse.cs`
- `Validators/LoginValidator.cs`
- `Validators/ComercianteValidator.cs`

### Infrastructure
- `Data/GesComercioDbContext.cs`
- `Repositories/UsuarioRepository.cs`
- `Repositories/MunicipioRepository.cs`
- `Repositories/ComercianteRepository.cs`
- `Services/TokenService.cs`
- `Services/AuthService.cs`

### API
- `Controllers/AuthController.cs`
- `Program.cs` configurado
- `appsettings.json` configurado

## Verificación
- Swagger UI funciona ✅
- POST /api/Auth/login retorna token JWT ✅
- BCrypt verifica passwords correctamente ✅
- Token expira en 1 hora exacta ✅
- CORS configurado para Angular ✅