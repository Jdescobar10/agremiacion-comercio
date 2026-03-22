# Reto 03 — Data Semilla
**Estado:** ✅ Completado y verificado
**Fecha:** 2026-03
**Script:** `db/03_data_semilla.sql`

---

## Datos Insertados

| Tabla | Total |
|---|---|
| Municipios | 10 |
| Usuarios | 2 |
| Comerciantes | 5 |
| Establecimientos | 10 |

## Decisiones Técnicas
- Passwords hasheados con BCrypt cost factor 12
- 4 comerciantes activos + 1 inactivo para probar filtros
- Establecimientos distribuidos desigualmente para probar ordenamiento
- Municipios reales de Colombia

## Credenciales de Prueba
- Admin: admin@gescomercio.com / Admin123*
- Auxiliar: auxiliar@gescomercio.com / Auxiliar123*

## Verificación
- 10 municipios ✅
- 2 usuarios ✅
- 5 comerciantes ✅
- 10 establecimientos ✅