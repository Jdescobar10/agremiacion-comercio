-- ============================================================
-- GesComercio - Script 01: Modelo de Datos
-- Ejecutar en orden. Requiere SQL Server 2019+
-- ============================================================

-- ------------------------------------------------------------
-- 0. Crear y usar la base de datos
-- ------------------------------------------------------------
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'GesComercio')
BEGIN
    CREATE DATABASE GesComercio;
END
GO

USE GesComercio;
GO

-- ------------------------------------------------------------
-- 1. Tabla: Municipio (catálogo — SIN Departamento)
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.Municipio', 'U') IS NOT NULL
    DROP TABLE dbo.Municipio;
GO

CREATE TABLE dbo.Municipio (
    Id      INT          NOT NULL IDENTITY(1,1),
    Nombre  VARCHAR(100) NOT NULL,

    CONSTRAINT PK_Municipio PRIMARY KEY (Id)
);
GO

-- ------------------------------------------------------------
-- 2. Tabla: Usuario
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.Usuario', 'U') IS NOT NULL
    DROP TABLE dbo.Usuario;
GO

CREATE TABLE dbo.Usuario (
    Id                INT          NOT NULL IDENTITY(1,1),
    Nombre            VARCHAR(100) NOT NULL,
    CorreoElectronico VARCHAR(100) NOT NULL,
    Contrasena        VARCHAR(255) NOT NULL,  -- Siempre se almacena hash
    Rol               VARCHAR(30)  NOT NULL,  -- 'Administrador' | 'AuxiliarRegistro'

    CONSTRAINT PK_Usuario        PRIMARY KEY (Id),
    CONSTRAINT UQ_Usuario_Correo UNIQUE (CorreoElectronico),
    CONSTRAINT CK_Usuario_Rol    CHECK (Rol IN ('Administrador', 'AuxiliarRegistro'))
);
GO

-- ------------------------------------------------------------
-- 3. Tabla: Comerciante
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.Comerciante', 'U') IS NOT NULL
    DROP TABLE dbo.Comerciante;
GO

CREATE TABLE dbo.Comerciante (
    Id                INT          NOT NULL IDENTITY(1,1),
    RazonSocial VARCHAR(150) NOT NULL,
    MunicipioId       INT          NOT NULL,
    Telefono          VARCHAR(20)  NULL,       -- Opcional
    CorreoElectronico VARCHAR(100) NULL,        -- Opcional
    FechaRegistro     DATETIME2    NOT NULL DEFAULT GETUTCDATE(),
    Estado            BIT          NOT NULL DEFAULT 1,  -- 1=Activo, 0=Inactivo
    -- Auditoría (gestionada por trigger, nunca por body)
    ActualizadoEn     DATETIME2    NOT NULL DEFAULT GETUTCDATE(),
    ActualizadoPor    VARCHAR(100) NULL,

    CONSTRAINT PK_Comerciante           PRIMARY KEY (Id),
    CONSTRAINT FK_Comerciante_Municipio FOREIGN KEY (MunicipioId)
        REFERENCES dbo.Municipio(Id)
);
GO

CREATE INDEX IX_Comerciante_Estado
    ON dbo.Comerciante(Estado);
GO

-- ------------------------------------------------------------
-- 4. Tabla: Establecimiento
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.Establecimiento', 'U') IS NOT NULL
    DROP TABLE dbo.Establecimiento;
GO

CREATE TABLE dbo.Establecimiento (
    Id              INT             NOT NULL IDENTITY(1,1),
    Nombre          VARCHAR(150)    NOT NULL,
    Ingresos        DECIMAL(18,2)   NOT NULL DEFAULT 0,
    NumeroEmpleados INT             NOT NULL DEFAULT 0,
    ComercianteId   INT             NOT NULL,
    -- Auditoría (gestionada por trigger, nunca por body)
    ActualizadoEn   DATETIME2       NOT NULL DEFAULT GETUTCDATE(),
    ActualizadoPor  VARCHAR(100)    NULL,

    CONSTRAINT PK_Establecimiento             PRIMARY KEY (Id),
    CONSTRAINT FK_Establecimiento_Comerciante FOREIGN KEY (ComercianteId)
        REFERENCES dbo.Comerciante(Id),
    CONSTRAINT CK_Establecimiento_Ingresos    CHECK (Ingresos >= 0),
    CONSTRAINT CK_Establecimiento_Empleados   CHECK (NumeroEmpleados >= 0)
);
GO

CREATE INDEX IX_Establecimiento_ComercianteId
    ON dbo.Establecimiento(ComercianteId);
GO

-- ------------------------------------------------------------
-- FIN Script 01 — Modelo de Datos
-- ============================================================
PRINT 'Script 01 ejecutado correctamente — Modelo de datos creado.';
GO