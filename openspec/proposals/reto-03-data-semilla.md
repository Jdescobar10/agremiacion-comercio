# Proposal — Reto 03: Data Semilla
**Estado:** ✅ Archivado
**Script generado:** `db/03_data_semilla.sql`

---

## Decisiones Técnicas

| # | Decisión | Razón |
|---|----------|-------|
| 001 | BCrypt cost factor 12 | Balance seguridad/performance |
| 002 | 1 comerciante inactivo | Probar filtros de estado en CRUD |
| 003 | Distribución desigual de establecimientos | Probar ordenamiento DESC en reporte |
| 004 | Municipios reales de Colombia | Datos representativos del dominio |
| 005 | Teléfono y correo NULL en Ferretería Los Andes | Validar campos opcionales |