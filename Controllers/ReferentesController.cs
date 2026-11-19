using Microsoft.AspNetCore.Mvc;
using SGE.DTOs.Referentes;
using SGE.Services.Interfaces;

namespace SGE.Controllers;

public class ReferentesController : Controller
{
    private readonly IReferenteService _referenteService;

    public ReferentesController(IReferenteService referenteService)
    {
        _referenteService = referenteService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
        var referentes = await _referenteService.ObtenerTodosAsync(
            cancellationToken);

        return View(referentes);
    }
    [HttpGet]
    public async Task<IActionResult> Detalle(
        long dni,
        CancellationToken cancellationToken)
    {
        var referente = await _referenteService.ObtenerDetallePorDniAsync(
            dni,
            cancellationToken);

        if (referente is null)
        {
            return NotFound();
        }

        return View(referente);
    }
    [HttpGet]
public async Task<IActionResult> Editar(
    long dni,
    CancellationToken cancellationToken)
{
    var referente =
        await _referenteService.ObtenerParaEditarPorDniAsync(
            dni,
            cancellationToken);

    if (referente is null)
    {
        return NotFound();
    }

    return View(referente);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Editar(
    EditarReferenteDto dto,
    CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var resultado = await _referenteService.EditarAsync(
            dto,
            cancellationToken);

        if (!resultado.Exito)
        {
            ModelState.AddModelError(
                string.Empty,
                resultado.Mensaje);

            return View(dto);
        }

        TempData["MensajeExito"] = resultado.Mensaje;

        return RedirectToAction(
            nameof(Detalle),
            new { dni = dto.Dni });
    }

    [HttpGet]
    public IActionResult Nuevo()
    {
        return View(new CrearReferenteDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Nuevo(
        CrearReferenteDto dto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var resultado = await _referenteService.CrearAsync(
            dto,
            cancellationToken);

        if (!resultado.Exito)
        {
            ModelState.AddModelError(
                string.Empty,
                resultado.Mensaje);

            return View(dto);
        }

        TempData["MensajeExito"] = resultado.Mensaje;

        return RedirectToAction(nameof(Index));
    }
}