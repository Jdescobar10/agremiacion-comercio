-- ============================================================
-- GesComercio - Script 02: Triggers de Auditoría
-- Prerequisito: Script 01 ejecutado correctamente
-- ============================================================

USE GesComercio;
GO

-- ------------------------------------------------------------
-- 1. Trigger: Comerciante — INSERT y UPDATE
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.trg_Comerciante_Auditoria', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_Comerciante_Auditoria;
GO

CREATE TRIGGER dbo.trg_Comerciante_Auditoria
ON dbo.Comerciante
AFTER INSERT, UPDATE
AS
BEGIN
    --Se utiliza para no enviar la cantidad de filas afectadas
    SET NOCOUNT ON;

    -- Evita recursiòn si el propio trigger dispara otra actualizaciòn
    IF TRIGGER_NESTLEVEL() > 1 RETURN;

    -- Actualiza las columnas de auditoría
    UPDATE dbo.Comerciante
    SET
        ActualizadoEn  = GETUTCDATE(),
        ActualizadoPor = i.ActualizadoPor
    FROM dbo.Comerciante c
    INNER JOIN inserted i ON c.Id = i.Id;
END;
GO

-- ------------------------------------------------------------
-- 2. Trigger: Establecimiento — INSERT y UPDATE
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.trg_Establecimiento_Auditoria', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_Establecimiento_Auditoria;
GO

CREATE TRIGGER dbo.trg_Establecimiento_Auditoria
ON dbo.Establecimiento
AFTER INSERT, UPDATE
AS
BEGIN

    --Se utiliza para no enviar la cantidad de filas afectadas
    SET NOCOUNT ON;

    -- Evita recursiòn si el propio trigger dispara otra actualizaciòn
    IF TRIGGER_NESTLEVEL() > 1 RETURN;

    UPDATE dbo.Establecimiento
    SET
        ActualizadoEn  = GETUTCDATE(),
        ActualizadoPor = i.ActualizadoPor
    FROM dbo.Establecimiento e
    INNER JOIN inserted i ON e.Id = i.Id;
END;
GO

-- ------------------------------------------------------------
-- FIN Script 02 — Triggers de Auditoría
-- ============================================================
PRINT 'Script 02 ejecutado correctamente — Triggers de auditoría creados.';
GO
