# Proposal — Reto 02: Triggers de Auditoría
**Estado:** ✅ Archivado
**Script generado:** `db/02_triggers_auditoria.sql`

---

## Decisiones Técnicas

| # | Decisión | Razón |
|---|----------|-------|
| 001 | Un trigger por tabla cubre INSERT y UPDATE | Más limpio que dos triggers separados |
| 002 | GETUTCDATE() para ActualizadoEn | Consistencia UTC con .NET |
| 003 | TRIGGER_NESTLEVEL() > 1 | Previene recursión infinita |
| 004 | AFTER INSERT UPDATE, no INSTEAD OF | No interfiere con lógica normal de DML |