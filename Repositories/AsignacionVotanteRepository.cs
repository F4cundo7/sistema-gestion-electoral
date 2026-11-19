using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Models.Entities;

namespace SGE.Repositories;

public class AsignacionVotanteRepository
    : IAsignacionVotanteRepository
{
    private readonly ApplicationDbContext _context;

    public AsignacionVotanteRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExisteAsignacionActivaPorPersonaAsync(
        int personaId,
        CancellationToken cancellationToken = default)
    {
        return await _context.AsignacionesVotantes
            .AnyAsync(
                asignacion =>
                    asignacion.PersonaId == personaId &&
                    asignacion.Activo,
                cancellationToken);
    }

    public async Task<List<AsignacionVotante>> ObtenerTodasAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.AsignacionesVotantes
            .AsNoTracking()
            .Include(asignacion => asignacion.Persona)
            .Include(asignacion => asignacion.Movilizador)
                .ThenInclude(movilizador => movilizador.Persona)
            .Include(asignacion => asignacion.Movilizador)
                .ThenInclude(movilizador => movilizador.Referente)
                    .ThenInclude(referente => referente.Persona)
            .OrderBy(asignacion =>
                asignacion.Persona.ApellidoNombre)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AsignacionVotante>>
        ObtenerPorMovilizadorAsync(
            int movilizadorId,
            CancellationToken cancellationToken = default)
    {
        return await _context.AsignacionesVotantes
            .AsNoTracking()
            .Include(asignacion => asignacion.Persona)
            .Include(asignacion => asignacion.Movilizador)
                .ThenInclude(movilizador => movilizador.Persona)
            .Include(asignacion => asignacion.Movilizador)
                .ThenInclude(movilizador => movilizador.Referente)
                    .ThenInclude(referente => referente.Persona)
            .Where(asignacion =>
                asignacion.MovilizadorId == movilizadorId)
            .OrderBy(asignacion =>
                asignacion.Persona.ApellidoNombre)
            .ToListAsync(cancellationToken);
    }

    public async Task<AsignacionVotante?>
        ObtenerPorDniVotanteAsync(
            long id,
            CancellationToken cancellationToken = default)
    {
        return await _context.AsignacionesVotantes
            .AsNoTracking()
            .Include(asignacion => asignacion.Persona)
            .Include(asignacion => asignacion.Movilizador)
                .ThenInclude(movilizador => movilizador.Persona)
            .Include(asignacion => asignacion.Movilizador)
                .ThenInclude(movilizador => movilizador.Referente)
                    .ThenInclude(referente => referente.Persona)
            .OrderByDescending(asignacion => asignacion.Activo)
            .ThenByDescending(asignacion => asignacion.FechaAsignacion)
            .FirstOrDefaultAsync(
                asignacion =>
                    asignacion.Persona.Id == id,
                cancellationToken);
    }

        public async Task<AsignacionVotante?>
        ObtenerParaEditarPorDniVotanteAsync(
            long id,
            CancellationToken cancellationToken = default)
        {
            return await _context.AsignacionesVotantes
                .Include(asignacion => asignacion.Persona)
                .Include(asignacion => asignacion.Movilizador)
                    .ThenInclude(movilizador => movilizador.Persona)
                .Include(asignacion => asignacion.Movilizador)
                    .ThenInclude(movilizador => movilizador.Referente)
                        .ThenInclude(referente => referente.Persona)
                .OrderByDescending(asignacion => asignacion.Activo)
                .ThenByDescending(asignacion => asignacion.FechaAsignacion)
                .FirstOrDefaultAsync(
                    asignacion =>
                        asignacion.Persona.Id == id,
                    cancellationToken);
        }

        public async Task AgregarAsync(
            AsignacionVotante asignacion,
            CancellationToken cancellationToken = default)
        {
            await _context.AsignacionesVotantes.AddAsync(
                asignacion,
                cancellationToken);
        }

        public async Task<int> GuardarCambiosAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(
                cancellationToken);
        }

    public Task<AsignacionVotante?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<AsignacionVotante?> ObtenerParaEditarPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}