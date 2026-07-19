using SGE.Models.Entities;

namespace SGE.Repositories;

public interface IAsignacionVotanteRepository
{
    Task<bool> ExistePorPersonaAsync(
        int personaId,
        CancellationToken cancellationToken = default);

    Task<List<AsignacionVotante>> ObtenerPorMovilizadorAsync(
        int movilizadorId,
        CancellationToken cancellationToken = default);

    Task AgregarAsync(
        AsignacionVotante asignacion,
        CancellationToken cancellationToken = default);

    Task<int> GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}