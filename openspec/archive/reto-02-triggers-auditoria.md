# Reto 02 — Triggers de Auditoría
**Estado:** ✅ Completado y verificado
**Fecha:** 2026-03
**Script:** `db/02_triggers_auditoria.sql`

---

## Triggers Creados

| Trigger | Tabla | Evento |
|---|---|---|
| `trg_Comerciante_Auditoria` | Comerciante | AFTER INSERT, UPDATE |
| `trg_Establecimiento_Auditoria` | Establecimiento | AFTER INSERT, UPDATE |

## Mejoras Aplicadas
- `SET NOCOUNT ON` para no retornar filas afectadas
- `IF TRIGGER_NESTLEVEL() > 1 RETURN` para evitar recursión

## Campos Gestionados
- `ActualizadoEn` → GETUTCDATE() automático
- `ActualizadoPor` → viene del contexto del backend vía EF Core

## Verificación
- 2 triggers activos ✅
- Deshabilitado = 0 en ambos ✅