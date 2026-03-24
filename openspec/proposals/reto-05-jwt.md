# Proposal — Reto 05: Autenticación JWT
**Estado:** ✅ Archivado
**Rama:** `feature/backend-reto-05-jwt`

---

## Decisiones Técnicas

| # | Decisión | Razón |
|---|----------|-------|
| 001 | Clean Architecture — 5 capas | Separación de responsabilidades y mantenibilidad |
| 002 | .NET 8 LTS | Estabilidad y soporte hasta noviembre 2026 |
| 003 | JWT HS256 | Simplicidad y compatibilidad con Angular |
| 004 | BCrypt para passwords | Estándar de la industria para hash seguro |
| 005 | FluentValidation | Validaciones limpias y reutilizables |
| 006 | Repository Pattern | Desacopla acceso a datos de la lógica de negocio |
| 007 | ApiResponse<T> | Estandariza todas las respuestas de la API |
| 008 | Token expira en 1h exacta | ClockSkew = TimeSpan.Zero |
| 009 | CORS solo para localhost:4200 | Seguridad — no wildcard en producción |

## Estructura de Capas
```
GesComercio.Domain          → Entidades, Interfaces, Enums
GesComercio.Application     → DTOs, Interfaces, Services, Validators, Common
GesComercio.Infrastructure  → DbContext, Repositories, Services (JWT, Auth)
GesComercio.API             → Controllers, Middleware, Program.cs
GesComercio.Tests           → xUnit, Moq, FluentAssertions
```

## Paquetes NuGet instalados
| Proyecto | Paquete |
|---|---|
| Infrastructure | EF Core 9, EF Core SqlServer, EF Core Tools, BCrypt.Net-Next |
| API | JwtBearer 8.x, Swashbuckle, FluentValidation.AspNetCore |
| Application | FluentValidation |
| Tests | Moq, FluentAssertions |