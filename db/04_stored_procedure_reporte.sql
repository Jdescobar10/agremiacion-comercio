-- ============================================================
-- GesComercio - Script 04: Stored Procedure Reporte
-- Prerequisito: Scripts 01, 02 y 03 ejecutados correctamente
-- ============================================================

USE GesComercio;
GO

-- ------------------------------------------------------------
-- Renombrar columna NombreRazonSocial a RazonSocial
-- ------------------------------------------------------------
IF EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('dbo.Comerciante')
    AND name = 'NombreRazonSocial'
)
BEGIN
    EXEC sp_rename 'dbo.Comerciante.NombreRazonSocial', 'RazonSocial', 'COLUMN';
END
GO

-- ------------------------------------------------------------
-- Stored Procedure: usp_Reporte_ComerciantesActivos
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.usp_Reporte_ComerciantesActivos', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_Reporte_ComerciantesActivos;
GO

CREATE PROCEDURE dbo.usp_Reporte_ComerciantesActivos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.RazonSocial                               AS RazonSocial,
        m.Nombre                                    AS Municipio,
        ISNULL(c.Telefono, 'N/A')                   AS Telefono,
        ISNULL(c.CorreoElectronico, 'N/A')          AS CorreoElectronico,
        c.FechaRegistro                             AS FechaRegistro,
        CASE c.Estado
            WHEN 1 THEN 'Activo'
            WHEN 0 THEN 'Inactivo'
        END                                         AS Estado,
        COUNT(e.Id)                                 AS CantidadEstablecimientos,
        ISNULL(SUM(e.Ingresos), 0)                  AS TotalIngresos,
        ISNULL(SUM(e.NumeroEmpleados), 0)           AS CantidadEmpleados

    FROM dbo.Comerciante c
    INNER JOIN dbo.Municipio m ON c.MunicipioId = m.Id
    LEFT JOIN  dbo.Establecimiento e ON c.Id = e.ComercianteId

    WHERE c.Estado = 1

    GROUP BY
        c.RazonSocial,
        m.Nombre,
        c.Telefono,
        c.CorreoElectronico,
        c.FechaRegistro,
        c.Estado

    ORDER BY
        CantidadEstablecimientos DESC;
END;
GO

-- ------------------------------------------------------------
-- FIN Script 04 — Stored Procedure Reporte
-- ============================================================
PRINT 'Script 04 ejecutado correctamente — SP usp_Reporte_ComerciantesActivos creado.';
GO