using GesComercio.Domain.Entities;
using GesComercio.Domain.Interfaces;
using GesComercio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GesComercio.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly GesComercioDbContext _context;

    public UsuarioRepository(GesComercioDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByCorreoAsync(string correo)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.CorreoElectronico == correo);
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}