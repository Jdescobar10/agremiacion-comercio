# Reto 01 — Modelo de Datos SQL Server
**Estado:** ✅ Completado y verificado
**Fecha:** 2026-03
**Script:** `db/01_modelo_datos.sql`

---

## Decisiones Técnicas

| # | Decisión | Razón |
|---|----------|-------|
| 001 | PKs tipo IDENTITY | Requisito explícito del proyecto |
| 002 | Municipio como tabla catálogo independiente | El Reto 06 requiere endpoint de lista de municipios |
| 003 | Departamento eliminado | No estaba en los requisitos — se removió tras verificación |
| 004 | Teléfono y CorreoElectronico NULL en Comerciante | Son campos opcionales según requisitos |
| 005 | Estado BIT en Comerciante | Representa Activo=1 / Inactivo=0 |
| 006 | DECIMAL(18,2) en Ingresos | Requisito explícito de dos decimales |
| 007 | Campos auditoría presentes desde modelo | Los triggers del Reto 02 los gestionarán |

---

## Tablas Creadas

| Tabla | Tipo | Columnas Clave |
|---|---|---|
| `Municipio` | Catálogo | Id, Nombre |
| `Usuario` | Principal | Id, Nombre, CorreoElectronico, Contrasena, Rol |
| `Comerciante` | Principal | Id, NombreRazonSocial, MunicipioId, Telefono, CorreoElectronico, FechaRegistro, Estado, ActualizadoEn, ActualizadoPor |
| `Establecimiento` | Principal | Id, Nombre, Ingresos, NumeroEmpleados, ComercianteId, ActualizadoEn, ActualizadoPor |

---

## Restricciones Transversales Aplicadas
- PKs tipo Identity en todas las tablas ✅
- Campos de auditoría nunca se reciben por body ✅
- Scripts numerados para ejecución secuencial ✅

---

## Verificación
```sql
SELECT t.name AS Tabla, COUNT(c.name) AS TotalColumnas
FROM sys.tables t
JOIN sys.columns c ON t.object_id = c.object_id
WHERE t.name IN ('Municipio','Usuario','Comerciante','Establecimiento')
GROUP BY t.name
ORDER BY t.name;
-- Resultado esperado: 4 tablas
```

**Resultado obtenido:** 4 tablas ✅