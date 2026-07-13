using SGE.DTOs.Importacion;

namespace SGE.Services.Interfaces;

public interface IPadronImportService
{
    Task<ResultadoImportacionDto> ImportarAsync(
        Stream archivo,
        string nombreArchivo,
        CancellationToken cancellationToken = default);
}