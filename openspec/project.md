# PROJECT SPEC — Agremiación Nacional de Comercio
# Gestión de Comerciantes y Establecimientos
# Versión: 1.0 | Fecha: 2026-03 | Metodología: OpenSpec (Fission-AI)

---

## IDENTIDAD DEL SISTEMA

**Nombre:** GesComercio
**Propósito:** Aplicación interna para la agremiación nacional de comercio que permite
gestionar comerciantes y sus establecimientos, con control de acceso por roles.
**Estado:** En construcción — desarrollo guiado por Spec-Driven Development.

---

## STACK TECNOLÓGICO (versiones exactas)

| Capa       | Tecnología              | Versión      | Notas                              |
|------------|-------------------------|--------------|------------------------------------|
| Base datos | SQL Server              | 2019+        | T-SQL estándar, sin CLR            |
| ORM        | Entity Framework Core   | 8.x          | Code-First migrations              |
| Backend    | .NET                    | 8.0 (LTS)    | Clean Architecture + SOLID         |
| Auth       | JWT Bearer              | —            | Token 1h, RS256 o HS256            |
| Frontend   | Angular                 | 16+          | Standalone components permitidos   |
| CSS        | Angular Material o Tailwind | —        | A definir en Reto 09               |
| Testing BE | xUnit + NUnit           | Latest       | Ambos disponibles                  |
| Testing FE | Jest                    | 29+          | Con Testing Library                |
| Contenedor | Docker                  | —            | Dockerfile por servicio            |
| IDE        | Google Antigravity      | Preview 2025 | Fork VS Code, modelos Gemini 3 Pro |

---

## ARQUITECTURA

### Backend — Clean Architecture (capas en orden de dependencia)
```
GesComercio.Domain          ← Entidades, Value Objects, interfaces de repositorio
GesComercio.Application     ← Use Cases, DTOs, interfaces de servicios, validadores
GesComercio.Infrastructure  ← EF Core, SQL Server, JWT, implementaciones de repos
GesComercio.API             ← Controllers, Middleware, Swagger, Program.cs
GesComercio.Tests           ← xUnit, mocks, tests de integración
```

**Regla de oro:** Las capas internas (Domain, Application) NO referencian capas externas.
Dependency inversion siempre hacia adentro.

### Frontend — Estructura Angular
```
src/
├── app/
│   ├── core/           ← Guards, interceptors, servicios singleton
│   ├── shared/         ← Componentes, pipes y directivas reutilizables
│   ├── features/
│   │   ├── auth/       ← Login (Reto 09)
│   │   ├── merchants/  ← Home tabla + formulario (Retos 10-11)
│   │   └── reports/    ← Descarga CSV (Reto 10 parcial)
│   └── app.routes.ts   ← Routing con lazy loading
├── environments/
└── assets/
```

### Base de Datos — Esquema principal
```
dbo.Usuarios              ← Autenticación y roles
dbo.Comerciantes          ← Entidad principal
dbo.Establecimientos      ← Hijo de Comerciante (1:N)
dbo.AuditoriaLog (*)      ← Opcional, según diseño de triggers
```

---

## MODELO DE DOMINIO (referencia rápida)

### Usuario
- Id (PK, Identity), NombreUsuario, PasswordHash, Rol (Administrador | AuxiliarRegistro)
- CreadoEn, ActualizadoEn (auditoría)

### Comerciante
- Id (PK, Identity), NombreRazonSocial, Nit, Email, Telefono
- MunicipioId (FK referencia catálogo), Estado (Activo | Inactivo)
- CreadoEn, ActualizadoPor, ActualizadoEn (auditoría)

### Establecimiento
- Id (PK, Identity), Nombre, Ingresos (decimal), NumeroEmpleados (int)
- ComercianteId (FK), Estado (Activo | Inactivo)
- CreadoEn, ActualizadoPor, ActualizadoEn (auditoría)

### Municipio (catálogo)
- Id, Nombre, DepartamentoId (o nombre departamento directo)

---

## ROLES Y PERMISOS

| Acción                          | Administrador | AuxiliarRegistro |
|---------------------------------|:-------------:|:----------------:|
| Login                           | ✓             | ✓                |
| Ver lista comerciantes          | ✓             | ✓                |
| Crear comerciante               | ✓             | ✓                |
| Editar comerciante              | ✓             | ✓                |
| Cambiar estado (PATCH)          | ✓             | ✓                |
| **Eliminar comerciante**        | ✓             | ✗                |
| **Descargar reporte CSV**       | ✓             | ✗                |
| Ver municipios                  | ✓             | ✓                |

---

## RESTRICCIONES TRANSVERSALES (NON-NEGOTIABLE)

Estas restricciones aplican a TODOS los retos. El agente NO debe violarlas bajo ningún
contexto, aunque parezca más conveniente técnicamente:

### Seguridad
- **JWT obligatorio** en todos los endpoints excepto `POST /api/auth/login`
- **Credenciales indetectables** desde Network tab del browser (usar HttpOnly cookies
  o al menos no exponer el password en response body ni en logs)
- **OWASP compliance básico** en frontend: sanitización de inputs, no eval(), CSP headers
- **Roles aplicados en backend**, no solo en frontend (el frontend solo oculta UI)
- Tokens con **expiración de 1 hora exacta**

### Auditoría
- `CreadoEn`, `ActualizadoEn`, `ActualizadoPor` **nunca se reciben por request body**
- Se gestionan mediante: triggers SQL (DB) + interceptores/middleware (.NET) + EF interceptors
- El usuario que realiza la acción se extrae siempre del token JWT

### API
- **CORS** configurado explícitamente (no wildcard en producción)
- Respuestas paginadas con estructura consistente: `{ data: [], total: N, page: N, pageSize: N }`
- Errores con estructura consistente: `{ error: string, detail?: string, statusCode: N }`
- Reporte CSV con **separador pipe "|"**, no coma

### Base de Datos
- Scripts SQL **numerados** (01_, 02_, etc.) y ejecutables en orden secuencial
- PKs tipo **Identity** (no GUID, no secuencias manuales)
- Triggers de auditoría en tablas principales (no en catálogos)

### Frontend
- **Estado global** para el usuario autenticado (token + nombre + rol)
- Header visible en todas las páginas autenticadas mostrando nombre y rol
- Paginación con opciones: 5, 10, 15 items por página
- Formulario con validación en **todos** los campos: tipo, longitud mínima/máxima, obligatoriedad
- Sumatoria de ingresos y empleados visible en el **footer del formulario de edición**

---

## RETOS — MAPA DE DESARROLLO

### FASE 1 — SQL Server
- `reto-01` → Modelo de datos (tablas, PKs, FKs, índices)
- `reto-02` → Triggers de auditoría
- `reto-03` → Data semilla
- `reto-04` → Stored procedure reporte

### FASE 2 — .NET 8
- `reto-05` → Auth JWT + configuración Clean Architecture
- `reto-06` → Endpoint municipios (GET, privado, cache opcional)
- `reto-07` → CRUD Comerciantes (GET paginado, POST, PUT, PATCH, DELETE)
- `reto-08` → Endpoint reporte CSV (solo Administrador)

### FASE 3 — Angular 16+
- `reto-09` → Página Login
- `reto-10` → Página Home (tabla paginada, acciones por rol, CSV, logout)
- `reto-11` → Formulario creación/edición + sumatoria en edición

---

## CONVENCIONES DE CÓDIGO

### .NET
- Namespaces: `GesComercio.<Capa>.<Contexto>` (ej: `GesComercio.Application.Merchants`)
- Interfaces con prefijo `I` (ej: `IMerchantRepository`)
- DTOs con sufijo `Dto` o `Request`/`Response` según contexto
- Async/await en toda la cadena; nunca `.Result` ni `.Wait()`

### Angular
- Componentes: PascalCase + sufijo `Component`
- Servicios: PascalCase + sufijo `Service`, providedIn root o feature module
- Observables: sufijo `$` (ej: `merchants$`)
- Interfaces de modelos en `core/models/`

### SQL
- Nombres de tablas: PascalCase singular (ej: `Comerciante`)
- Stored procedures: `usp_<Contexto>_<Accion>` (ej: `usp_Reporte_ComerciantesActivos`)
- Triggers: `trg_<Tabla>_<Evento>` (ej: `trg_Comerciante_AfterUpdate`)

---

## INSTRUCCIÓN PARA EL AGENTE DE ANTIGRAVITY

Cuando recibas una tarea de este proyecto:
1. Lee SIEMPRE este archivo `openspec/project.md` antes de generar código
2. Verifica que tu output NO viole ninguna restricción transversal
3. Pregunta al desarrollador si algo contradice este spec antes de improvisar
4. Coloca los archivos en las rutas indicadas en la estructura de repositorio
5. Si generas migraciones EF, NO incluyas datos semilla en las migraciones — van en scripts SQL separados
6. El modelo principal para generación de código es **Gemini 3 Pro**; usa GPT-OSS solo si se indica

---

## HISTORIAL DE DECISIONES ARQUITECTÓNICAS (ADR)

| # | Decisión | Razón |
|---|----------|-------|
| 001 | PKs Identity sobre GUID | Simplicidad, performance en SQL Server, requisito explícito |
| 002 | Auditoría en triggers SQL | Garantía a nivel DB independiente de la capa de aplicación |
| 003 | JWT en Authorization header | Compatibilidad con Swagger y clientes REST estándar |
| 004 | Paginación server-side | Datasets potencialmente grandes; no cargar todo en cliente |
| 005 | CSV con separador pipe | Requisito explícito del negocio para compatibilidad con sistemas legacy |