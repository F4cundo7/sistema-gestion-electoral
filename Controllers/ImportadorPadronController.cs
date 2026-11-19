using Microsoft.AspNetCore.Mvc;
using SGE.DTOs.Importacion;
using SGE.Services.Interfaces;

namespace SGE.Controllers;

[Route("api/importador-padron")]
public class ImportadorPadronController : Controller
{
    private readonly IPadronImportService _padronImportService;

    public ImportadorPadronController(
        IPadronImportService padronImportService)
    {
        _padronImportService = padronImportService;
    }

    [HttpPost("importar")]
    public async Task<IActionResult> Importar(
        IFormFile archivo,
        CancellationToken cancellationToken)
    {
        if (archivo is null || archivo.Length == 0)
        {
            return BadRequest(new
            {
                mensaje = "Debe seleccionar un archivo Excel."
            });
        }

        await using Stream stream = archivo.OpenReadStream();

        ResultadoImportacionDto resultado =
            await _padronImportService.ImportarAsync(
                stream,
                archivo.FileName,
                cancellationToken);

        return Ok(resultado);
    }
}