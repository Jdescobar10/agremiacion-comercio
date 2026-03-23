using GesComercio.Domain.Entities;
using GesComercio.Domain.Interfaces;
using GesComercio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GesComercio.Infrastructure.Repositories;

public class MunicipioRepository : IMunicipioRepository
{
    private readonly GesComercioDbContext _context;

    public MunicipioRepository(GesComercioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Municipio>> GetAllAsync()
    {
        return await _context.Municipios
            .OrderBy(m => m.Nombre)
            .ToListAsync();
    }
}