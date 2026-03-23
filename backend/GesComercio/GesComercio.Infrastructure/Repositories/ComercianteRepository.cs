using GesComercio.Domain.Entities;
using GesComercio.Domain.Interfaces;
using GesComercio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GesComercio.Infrastructure.Repositories;

public class ComercianteRepository : IComercianteRepository
{
    private readonly GesComercioDbContext _context;

    public ComercianteRepository(GesComercioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Comerciante>> GetAllAsync(
        int page, int pageSize, string? filtro, bool? estado)
    {
        var query = _context.Comerciantes
            .Include(c => c.Municipio)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filtro))
            query = query.Where(c => c.RazonSocial.Contains(filtro));

        if (estado.HasValue)
            query = query.Where(c => c.Estado == estado.Value);

        return await query
            .OrderBy(c => c.RazonSocial)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalAsync(string? filtro, bool? estado)
    {
        var query = _context.Comerciantes.AsQueryable();

        if (!string.IsNullOrEmpty(filtro))
            query = query.Where(c => c.RazonSocial.Contains(filtro));

        if (estado.HasValue)
            query = query.Where(c => c.Estado == estado.Value);

        return await query.CountAsync();
    }

    public async Task<Comerciante?> GetByIdAsync(int id)
    {
        return await _context.Comerciantes
            .Include(c => c.Municipio)
            .Include(c => c.Establecimientos)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Comerciante> CreateAsync(Comerciante comerciante)
    {
        _context.Comerciantes.Add(comerciante);
        await _context.SaveChangesAsync();
        return comerciante;
    }

    public async Task<Comerciante> UpdateAsync(Comerciante comerciante)
    {
        _context.Comerciantes.Update(comerciante);
        await _context.SaveChangesAsync();
        return comerciante;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var comerciante = await _context.Comerciantes.FindAsync(id);
        if (comerciante == null) return false;

        _context.Comerciantes.Remove(comerciante);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> PatchEstadoAsync(int id, bool estado)
    {
        var comerciante = await _context.Comerciantes.FindAsync(id);
        if (comerciante == null) return false;

        comerciante.Estado = estado;
        await _context.SaveChangesAsync();
        return true;
    }
}