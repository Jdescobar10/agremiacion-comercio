using GesComercio.Application.Common;
using GesComercio.Application.DTOs;
using GesComercio.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GesComercio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]  // Requiere JWT — endpoint privado
public class MunicipioController : ControllerBase
{
    private readonly IMunicipioService _municipioService;

    public MunicipioController(IMunicipioService municipioService)
    {
        _municipioService = municipioService;
    }

    /// <summary>
    /// Retorna lista de municipios — requiere JWT
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var municipios = await _municipioService.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<MunicipioDto>>.Ok(municipios));
    }
}