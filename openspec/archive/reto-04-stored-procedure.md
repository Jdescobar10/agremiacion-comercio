# Reto 04 — Stored Procedure Reporte
**Estado:** ✅ Completado y verificado
**Fecha:** 2026-03
**Script:** `db/04_stored_procedure_reporte.sql`

---

## SP Creado
`usp_Reporte_ComerciantesActivos`

## Decisiones Técnicas
- LEFT JOIN para incluir comerciantes sin establecimientos
- ISNULL para campos opcionales (Telefono, CorreoElectronico)
- ORDER BY CantidadEstablecimientos DESC
- Funciones auxiliares no necesarias — agregaciones simples
- Renombrado NombreRazonSocial → RazonSocial

## Verificación
- 4 comerciantes activos retornados ✅
- Papelería Punto excluida por Estado=0 ✅
- Ordenamiento DESC correcto ✅
- Totales calculados correctamente ✅