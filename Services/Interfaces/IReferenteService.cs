using SGE.DTOs.Compartidos;
using SGE.DTOs.Referentes;

namespace SGE.Services.Interfaces;

public interface IReferenteService
{
    Task<List<ReferenteListadoDto>> ObtenerTodosAsync(
        CancellationToken cancellationToken = default);

    Task<ReferenteDetalleDto?> ObtenerDetallePorDniAsync(
        long dni,
        CancellationToken cancellationToken = default);

    Task<EditarReferenteDto?> ObtenerParaEditarPorDniAsync(
    long dni,
    CancellationToken cancellationToken = default);

    Task<ResultadoOperacionDto> CrearAsync(
        CrearReferenteDto dto,
        CancellationToken cancellationToken = default);
    Task<ResultadoOperacionDto> EditarAsync(
        EditarReferenteDto dto,
        CancellationToken cancellationToken = default);
}