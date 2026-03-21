# Proposal — Reto 01: Modelo de Datos
**Estado:** ✅ Archivado (ver archive/)
**Script generado:** `db/01_modelo_datos.sql`

---

## Entidades del Sistema

### Usuario
- Id (PK, Identity)
- Nombre VARCHAR(100) NOT NULL
- CorreoElectronico VARCHAR(100) NOT NULL UNIQUE
- Contrasena VARCHAR(255) NOT NULL — siempre hash
- Rol VARCHAR(30) — 'Administrador' | 'AuxiliarRegistro'

### Comerciante
- Id (PK, Identity)
- NombreRazonSocial VARCHAR(150) NOT NULL
- MunicipioId INT FK → Municipio
- Telefono VARCHAR(20) NULL
- CorreoElectronico VARCHAR(100) NULL
- FechaRegistro DATETIME2 NOT NULL
- Estado BIT NOT NULL — 1=Activo, 0=Inactivo
- ActualizadoEn DATETIME2 — gestionado por trigger
- ActualizadoPor VARCHAR(100) — gestionado por trigger

### Establecimiento
- Id (PK, Identity)
- Nombre VARCHAR(150) NOT NULL
- Ingresos DECIMAL(18,2) NOT NULL
- NumeroEmpleados INT NOT NULL
- ComercianteId INT FK → Comerciante
- ActualizadoEn DATETIME2 — gestionado por trigger
- ActualizadoPor VARCHAR(100) — gestionado por trigger

### Municipio (catálogo)
- Id (PK, Identity)
- Nombre VARCHAR(100) NOT NULL
```

---
