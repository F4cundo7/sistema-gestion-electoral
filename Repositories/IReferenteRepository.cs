using SGE.Models.Entities;

namespace SGE.Repositories;

public interface IReferenteRepository
{
    Task<bool> ExistePorPersonaAsync(
        int personaId,
        CancellationToken cancellationToken = default);

    Task<List<Referente>> ObtenerTodosAsync(
        CancellationToken cancellationToken = default);

    Task<Referente?> ObtenerPorDniAsync(
        long dni,
        CancellationToken cancellationToken = default);

    Task<Referente?> ObtenerParaEditarPorDniAsync(
        long dni,
        CancellationToken cancellationToken = default);

    Task AgregarAsync(
        Referente referente,
        CancellationToken cancellationToken = default);

    void Actualizar(Referente referente);

    Task<int> GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}