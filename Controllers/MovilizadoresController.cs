using Microsoft.AspNetCore.Mvc;
using SGE.DTOs.Movilizadores;
using SGE.Services.Interfaces;

namespace SGE.Controllers;

public class MovilizadoresController : Controller
{
    private readonly IMovilizadorService _movilizadorService;

    public MovilizadoresController(IMovilizadorService movilizadorService)
    {
        _movilizadorService = movilizadorService;
    }
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var movilizadores =
            await _movilizadorService.ObtenerTodosAsync(cancellationToken);

        return View(movilizadores);
    }
    public IActionResult Nuevo()
    {
        return View(new CrearMovilizadorDto());
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Nuevo(
        CrearMovilizadorDto dto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            await _movilizadorService.CrearAsync(
                dto,
                cancellationToken);

            TempData["Success"] =
                "Movilizador registrado correctamente.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(
                string.Empty,
                ex.Message);

            return View(dto);
        }
    }
    public async Task<IActionResult> Detalle(
    long dni,
    CancellationToken cancellationToken)
    {
        var movilizador =
            await _movilizadorService
                .ObtenerDetallePorDniAsync(
                    dni,
                    cancellationToken);

        if (movilizador is null)
        {
            return NotFound();
        }

        return View(movilizador);
    }
    public async Task<IActionResult> Editar(
    long dni,
    CancellationToken cancellationToken)
    {
        var dto =
            await _movilizadorService
                .ObtenerParaEditarPorDniAsync(
                    dni,
                    cancellationToken);

        if (dto is null)
        {
            return NotFound();
        }

        return View(dto);
    }
    [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Editar(
    EditarMovilizadorDto dto,
    CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            await _movilizadorService.EditarAsync(
                dto,
                cancellationToken);

            TempData["Success"] =
                "Movilizador actualizado correctamente.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(
                string.Empty,
                ex.Message);

            return View(dto);
        }
    }

}