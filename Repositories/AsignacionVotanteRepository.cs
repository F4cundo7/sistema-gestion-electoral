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

    public async Task<bool> ExistePorPersonaAsync(
        int personaId,
        CancellationToken cancellationToken = default)
    {
        return await _context.AsignacionesVotantes
            .AnyAsync(
                x => x.PersonaId == personaId,
                cancellationToken);
    }

    public async Task<List<AsignacionVotante>>
        ObtenerPorMovilizadorAsync(
        int movilizadorId,
        CancellationToken cancellationToken = default)
    {
        return await _context.AsignacionesVotantes
            .AsNoTracking()
            .Include(x => x.Persona)
            .Where(x => x.MovilizadorId == movilizadorId)
            .OrderBy(x => x.Persona.ApellidoNombre)
            .ToListAsync(cancellationToken);
    }

    public async Task AgregarAsync(
        AsignacionVotante asignacion,
        CancellationToken cancellationToken = default)
    {
        await _context.AsignacionesVotantes
            .AddAsync(asignacion, cancellationToken);
    }

    public async Task<int> GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context
            .SaveChangesAsync(cancellationToken);
    }
}