using SGE.Models.Entities;

namespace SGE.Repositories;

public interface IPersonaRepository
{
    Task<Persona?> ObtenerPorDniAsync(
        long dni,
        CancellationToken cancellationToken = default);

    Task<Dictionary<long, Persona>> ObtenerPorDnisAsync(
        IEnumerable<long> dnis,
        CancellationToken cancellationToken = default);

    Task AgregarRangoAsync(
        IEnumerable<Persona> personas,
        CancellationToken cancellationToken = default);

    void ActualizarRango(
        IEnumerable<Persona> personas);

    Task<int> GuardarCambiosAsync(
        CancellationToken cancellationToken = default);

    void LimpiarSeguimiento();
}