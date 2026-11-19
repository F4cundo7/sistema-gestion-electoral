using SGE.Models.Entities;

namespace SGE.Repositories;

public interface IAsignacionVotanteRepository
{
    Task<bool> ExisteAsignacionActivaPorPersonaAsync(
        int personaId,
        CancellationToken cancellationToken = default);

    Task<List<AsignacionVotante>> ObtenerTodasAsync(
        CancellationToken cancellationToken = default);

    Task<List<AsignacionVotante>> ObtenerPorMovilizadorAsync(
        int movilizadorId,
        CancellationToken cancellationToken = default);

    Task<AsignacionVotante?> ObtenerPorIdAsync(
        int id,
        CancellationToken cancellationToken = default);
    Task<AsignacionVotante?> ObtenerParaEditarPorIdAsync(
        int id,
        CancellationToken cancellationToken = default);
    Task AgregarAsync(
        AsignacionVotante asignacion,
        CancellationToken cancellationToken = default);

    Task<int> GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}