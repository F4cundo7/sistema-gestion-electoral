using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Models;

namespace SGE.Repositories;

public class PersonaRepository : IPersonaRepository
{
    private readonly ApplicationDbContext _context;

    public PersonaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Dictionary<long, Persona>> ObtenerPorDnisAsync(
        IEnumerable<long> dnis,
        CancellationToken cancellationToken = default)
    {
        long[] dnisUnicos = dnis
            .Distinct()
            .ToArray();

        return await _context.Personas
            .Where(persona => dnisUnicos.Contains(persona.Dni))
            .ToDictionaryAsync(
                persona => persona.Dni,
                cancellationToken);
    }

    public async Task AgregarRangoAsync(
        IEnumerable<Persona> personas,
        CancellationToken cancellationToken = default)
    {
        await _context.Personas.AddRangeAsync(
            personas,
            cancellationToken);
    }

    public void ActualizarRango(IEnumerable<Persona> personas)
    {
        _context.Personas.UpdateRange(personas);
    }

    public async Task<int> GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void LimpiarSeguimiento()
    {
        _context.ChangeTracker.Clear();
    }
}