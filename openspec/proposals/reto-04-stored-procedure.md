# Proposal — Reto 04: Stored Procedure Reporte
**Estado:** ✅ Archivado
**Script generado:** `db/04_stored_procedure_reporte.sql`

---

## Decisiones Técnicas

| # | Decisión | Razón |
|---|----------|-------|
| 001 | LEFT JOIN a Establecimiento | Incluir comerciantes sin establecimientos |
| 002 | ISNULL para Telefono y Correo | Campos opcionales — mostrar N/A si vacíos |
| 003 | CASE para Estado | Mostrar texto legible en lugar de BIT |
| 004 | Sin parámetros | Requerimiento simple sin filtros |
| 005 | Sin funciones auxiliares | Agregaciones simples no lo requieren |
| 006 | Renombrado NombreRazonSocial → RazonSocial | Ajuste al requerimiento del negocio |

## Columnas del Reporte
| Columna | Fuente |
|---|---|
| RazonSocial | Comerciante.RazonSocial |
| Municipio | Municipio.Nombre |
| Telefono | Comerciante.Telefono |
| CorreoElectronico | Comerciante.CorreoElectronico |
| FechaRegistro | Comerciante.FechaRegistro |
| Estado | CASE 1=Activo / 0=Inactivo |
| CantidadEstablecimientos | COUNT(Establecimiento.Id) |
| TotalIngresos | SUM(Establecimiento.Ingresos) |
| CantidadEmpleados | SUM(Establecimiento.NumeroEmpleados) |