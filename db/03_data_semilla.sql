-- ============================================================
-- GesComercio - Script 03: Data Semilla
-- Prerequisito: Scripts 01 y 02 ejecutados correctamente
-- ============================================================

USE GesComercio;
GO

-- ------------------------------------------------------------
-- 1. Municipios (catálogo base)
-- ------------------------------------------------------------
INSERT INTO dbo.Municipio (Nombre) VALUES
('Bogotá'),
('Medellín'),
('Cali'),
('Barranquilla'),
('Cartagena'),
('Bucaramanga'),
('Pereira'),
('Manizales'),
('Cúcuta'),
('Santa Marta');
GO

-- ------------------------------------------------------------
-- 2. Usuarios
-- Hash generado con BCrypt (cost factor 12)
-- Contraseña Admin123*    → usuario administrador
-- Contraseña Auxiliar123* → usuario auxiliar
-- ------------------------------------------------------------
INSERT INTO dbo.Usuario (Nombre, CorreoElectronico, Contrasena, Rol)
VALUES
(
    'Juan Administrador',
    'admin@gescomercio.com',
    '$2a$12$a99QtOXom067a8/bfY32duBqXWsrBXFCPpgcKnQ/gF.joxpEM05bO',  -- Admin123*
    'Administrador'
),
(
    'María Auxiliar',
    'auxiliar@gescomercio.com',
    '$2a$12$sDPxnZTaXpHVDHeV.Wkv5ugoQVJEW.x0sjA6VTP3QPmT3Kwm3crQm',  -- Auxiliar123*
    'AuxiliarRegistro'
);
GO

-- ------------------------------------------------------------
-- 3. Comerciantes (5 registros — 4 activos, 1 inactivo)
-- ------------------------------------------------------------
INSERT INTO dbo.Comerciante
    (NombreRazonSocial, MunicipioId, Telefono, CorreoElectronico, FechaRegistro, Estado, ActualizadoPor)
VALUES
(
    'Distribuidora El Éxito S.A.S',
    1,  -- Bogotá
    '6017891234',
    'contacto@exito.com',
    GETUTCDATE(),
    1,  -- Activo
    'admin@gescomercio.com'
),
(
    'Textiles del Valle Ltda',
    3,  -- Cali
    '6024567890',
    'info@textilesvalle.com',
    GETUTCDATE(),
    1,  -- Activo
    'admin@gescomercio.com'
),
(
    'Ferretería Los Andes',
    2,  -- Medellín
    NULL,
    NULL,
    GETUTCDATE(),
    1,  -- Activo
    'admin@gescomercio.com'
),
(
    'Supermercado La Cosecha',
    4,  -- Barranquilla
    '6053456789',
    'lacosecha@gmail.com',
    GETUTCDATE(),
    1,  -- Activo
    'admin@gescomercio.com'
),
(
    'Papelería y Miscelánea Punto',
    6,  -- Bucaramanga
    '6076543210',
    'puntopapeleria@outlook.com',
    GETUTCDATE(),
    0,  -- Inactivo (para probar filtros)
    'admin@gescomercio.com'
);
GO

-- ------------------------------------------------------------
-- 4. Establecimientos (10 registros distribuidos)
-- Comerciante 1 → 3 establecimientos
-- Comerciante 2 → 2 establecimientos
-- Comerciante 3 → 2 establecimientos
-- Comerciante 4 → 2 establecimientos
-- Comerciante 5 → 1 establecimiento
-- ------------------------------------------------------------
INSERT INTO dbo.Establecimiento
    (Nombre, Ingresos, NumeroEmpleados, ComercianteId, ActualizadoPor)
VALUES
-- Distribuidora El Éxito (3 establecimientos)
('Éxito Norte',         85000000.00,  45, 1, 'admin@gescomercio.com'),
('Éxito Centro',        120000000.00, 60, 1, 'admin@gescomercio.com'),
('Éxito Sur',           67500000.00,  32, 1, 'admin@gescomercio.com'),

-- Textiles del Valle (2 establecimientos)
('Sede Principal Valle', 95000000.00, 38, 2, 'admin@gescomercio.com'),
('Outlet Valle',         43000000.00, 18, 2, 'admin@gescomercio.com'),

-- Ferretería Los Andes (2 establecimientos)
('Los Andes Laureles',   55000000.00, 22, 3, 'admin@gescomercio.com'),
('Los Andes Envigado',   38000000.50, 15, 3, 'admin@gescomercio.com'),

-- Supermercado La Cosecha (2 establecimientos)
('La Cosecha Manga',     72000000.00, 28, 4, 'admin@gescomercio.com'),
('La Cosecha Bello',     61500000.75, 25, 4, 'admin@gescomercio.com'),

-- Papelería Punto (1 establecimiento)
('Punto Centro',         18000000.00, 8,  5, 'admin@gescomercio.com');
GO

-- ------------------------------------------------------------
-- FIN Script 03 — Data Semilla
-- ============================================================
PRINT 'Script 03 ejecutado correctamente — Data semilla insertada.';
GO