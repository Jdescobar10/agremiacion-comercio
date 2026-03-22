# Reto 04 — Stored Procedure Reporte
**Estado:** ✅ Completado y verificado
**Fecha:** 2026-03
**Script:** `db/04_stored_procedure_reporte.sql`

---

## SP Creado
`usp_Reporte_ComerciantesActivos`

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

## Decisiones Técnicas
- LEFT JOIN para incluir comerciantes sin establecimientos
- ISNULL para campos opcionales (Telefono, CorreoElectronico)
- ORDER BY CantidadEstablecimientos DESC con desempate alfabético
- Funciones auxiliares no necesarias — agregaciones simples

## Verificación
- 4 comerciantes activos retornados ✅
- Papelería Punto excluida por Estado=0 ✅
- Ordenamiento DESC correcto ✅
- Totales calculados correctamente ✅