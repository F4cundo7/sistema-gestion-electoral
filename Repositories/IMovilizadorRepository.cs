using SGE.Models.Entities;

namespace SGE.Repositories;

public interface IMovilizadorRepository
{
    Task<bool> ExistePorPersonaAsync(
        int personaId,
        CancellationToken cancellationToken = default);

    Task<Movilizador?> ObtenerPorIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<List<Movilizador>> ObtenerPorReferenteAsync(
        int referenteId,
        CancellationToken cancellationToken = default);

    Task AgregarAsync(
        Movilizador movilizador,
        CancellationToken cancellationToken = default);

    Task<int> GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}