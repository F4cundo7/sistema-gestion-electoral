using SGE.DTOs.Movilizadores;

namespace SGE.Services.Interfaces;

public interface IMovilizadorService
{
    Task<List<MovilizadorListadoDto>> ObtenerTodosAsync(
        CancellationToken cancellationToken = default);

    Task<List<MovilizadorListadoDto>> ObtenerPorReferenteAsync(
        long dniReferente,
        CancellationToken cancellationToken = default);

    Task<MovilizadorDetalleDto?> ObtenerDetallePorDniAsync(
        long dni,
        CancellationToken cancellationToken = default);

    Task<EditarMovilizadorDto?> ObtenerParaEditarPorDniAsync(
        long dni,
        CancellationToken cancellationToken = default);

    Task CrearAsync(
        CrearMovilizadorDto dto,
        CancellationToken cancellationToken = default);

    Task EditarAsync(
        EditarMovilizadorDto dto,
        CancellationToken cancellationToken = default);
}