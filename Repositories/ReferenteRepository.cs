using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Models.Entities;

namespace SGE.Repositories;

public class ReferenteRepository : IReferenteRepository
{
    private readonly ApplicationDbContext _context;

    public ReferenteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistePorPersonaAsync(
        int personaId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Referentes
            .AnyAsync(
                referente => referente.PersonaId == personaId,
                cancellationToken);
    }
    public async Task<List<Referente>> ObtenerTodosAsync(
    CancellationToken cancellationToken = default)
    {
        return await _context.Referentes
            .AsNoTracking()
            .Include(referente => referente.Persona)
            .OrderBy(referente => referente.Persona.ApellidoNombre)
            .ToListAsync(cancellationToken);
    }
    public async Task<Referente?> ObtenerPorDniAsync(
    long dni,
    CancellationToken cancellationToken = default)
    {
        return await _context.Referentes
            .AsNoTracking()
            .Include(referente => referente.Persona)
            .Include(referente => referente.Movilizadores)
            .FirstOrDefaultAsync(
                referente => referente.Persona.Dni == dni,
                cancellationToken);
    }
    public async Task<Referente?> ObtenerParaEditarPorDniAsync(
    long dni,
    CancellationToken cancellationToken = default)
    {
        return await _context.Referentes
            .Include(referente => referente.Persona)
            .FirstOrDefaultAsync(
                referente => referente.Persona.Dni == dni,
                cancellationToken);
    }

    public void Actualizar(Referente referente)
    {
        _context.Referentes.Update(referente);
    }
    public async Task AgregarAsync(
        Referente referente,
        CancellationToken cancellationToken = default)
    {
        await _context.Referentes.AddAsync(
            referente,
            cancellationToken);
    }

    public async Task<int> GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}