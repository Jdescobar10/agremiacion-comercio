# Proposal — Reto 06: Endpoint Municipios
**Estado:** ✅ Archivado
**Rama:** `feature/backend-reto-06-municipios`

---

## Decisiones Técnicas

| # | Decisión | Razón |
|---|----------|-------|
| 001 | Endpoint privado con [Authorize] | Requisito explícito — solo usuarios autenticados |
| 002 | Cache en memoria 1 hora | Municipios son datos estáticos — evita consultas repetidas a BD |
| 003 | Ordenado alfabéticamente | Mejor UX para el dropdown en Angular |
| 004 | IMemoryCache nativo de .NET | Sin dependencias externas — suficiente para este caso |