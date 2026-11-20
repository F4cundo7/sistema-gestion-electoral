using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Models.Entities;

namespace SGE.Repositories;

public class MovilizadorRepository : IMovilizadorRepository
{
    private readonly ApplicationDbContext _context;

    public MovilizadorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistePorPersonaAsync(
        int personaId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Movilizadores
            .AnyAsync(
                movilizador => movilizador.PersonaId == personaId,
                cancellationToken);
    }

    public async Task<List<Movilizador>> ObtenerTodosAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Movilizadores
            .AsNoTracking()
            .Include(movilizador => movilizador.Persona)
            .Include(movilizador => movilizador.Referente)
                .ThenInclude(referente => referente.Persona)
            .Include(movilizador => movilizador.Votantes)
            .OrderBy(movilizador => movilizador.Persona.ApellidoNombre)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Movilizador>> ObtenerPorReferenteAsync(
        int referenteId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Movilizadores
            .AsNoTracking()
            .Include(movilizador => movilizador.Persona)
            .Include(movilizador => movilizador.Referente)
                .ThenInclude(referente => referente.Persona)
            .Include(movilizador => movilizador.Votantes)
            .Where(movilizador => movilizador.ReferenteId == referenteId)
            .OrderBy(movilizador => movilizador.Persona.ApellidoNombre)
            .ToListAsync(cancellationToken);
    }

    public async Task<Movilizador?> ObtenerPorDniAsync(
        long dni,
        CancellationToken cancellationToken = default)
    {
        return await _context.Movilizadores
            .AsNoTracking()
            .Include(movilizador => movilizador.Persona)
            .Include(movilizador => movilizador.Referente)
                .ThenInclude(referente => referente.Persona)
            .Include(movilizador => movilizador.Votantes)
            .FirstOrDefaultAsync(
                movilizador => movilizador.Persona.Dni == dni,
                cancellationToken);
    }

    public async Task<Movilizador?> ObtenerParaEditarPorDniAsync(
        long dni,
        CancellationToken cancellationToken = default)
    {
        return await _context.Movilizadores
            .Include(movilizador => movilizador.Persona)
            .Include(movilizador => movilizador.Referente)
                .ThenInclude(referente => referente.Persona)
            .FirstOrDefaultAsync(
                movilizador => movilizador.Persona.Dni == dni,
                cancellationToken);
    }

    public async Task AgregarAsync(
        Movilizador movilizador,
        CancellationToken cancellationToken = default)
    {
        await _context.Movilizadores.AddAsync(
            movilizador,
            cancellationToken);
    }

    public async Task<int> GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}