using GesComercio.Application.DTOs;
using GesComercio.Application.Interfaces;
using GesComercio.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace GesComercio.Infrastructure.Services;

public class MunicipioService : IMunicipioService
{
    private readonly IMunicipioRepository _municipioRepository;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "municipios";

    public MunicipioService(
        IMunicipioRepository municipioRepository,
        IMemoryCache cache)
    {
        _municipioRepository = municipioRepository;
        _cache = cache;
    }

    public async Task<IEnumerable<MunicipioDto>> GetAllAsync()
    {
        // Intentar obtener desde cache
        if (_cache.TryGetValue(CacheKey, out IEnumerable<MunicipioDto>? cached))
            return cached!;

        // Si no está en cache, consultar BD
        var municipios = await _municipioRepository.GetAllAsync();

        var result = municipios.Select(m => new MunicipioDto
        {
            Id = m.Id,
            Nombre = m.Nombre
        });

        // Guardar en cache por 1 hora
        _cache.Set(CacheKey, result, TimeSpan.FromHours(1));

        return result;
    }
}