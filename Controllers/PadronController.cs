using Microsoft.AspNetCore.Mvc;
using SGE.DTOs.Importacion;
using SGE.Services.Interfaces;

namespace SGE.Controllers;

public class PadronController : Controller
{
    private readonly IPadronImportService _padronImportService;

    public PadronController(
        IPadronImportService padronImportService)
    {
        _padronImportService = padronImportService;
    }

    [HttpGet]
    public IActionResult Importar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequestSizeLimit(100_000_000)]
    public async Task<IActionResult> Importar(
        IFormFile? archivo,
        CancellationToken cancellationToken)
    {
        if (archivo is null || archivo.Length == 0)
        {
            ModelState.AddModelError(
                nameof(archivo),
                "Debe seleccionar un archivo Excel.");

            return View();
        }

        try
        {
            await using Stream stream = archivo.OpenReadStream();

            ResultadoImportacionDto resultado =
                await _padronImportService.ImportarAsync(
                    stream,
                    archivo.FileName,
                    cancellationToken);

            return View("Resultado", resultado);
        }
        catch (OperationCanceledException)
        {
            return StatusCode(
                StatusCodes.Status408RequestTimeout);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(
                nameof(archivo),
                ex.Message);

            return View();
        }
    }
}