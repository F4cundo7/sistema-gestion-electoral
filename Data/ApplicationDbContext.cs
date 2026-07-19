using Microsoft.EntityFrameworkCore;
using SGE.Models.Entities;

namespace SGE.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Persona> Personas => Set<Persona>();
    public DbSet<Referente> Referentes => Set<Referente>();
    public DbSet<Movilizador> Movilizadores => Set<Movilizador>();
    public DbSet<AsignacionVotante> AsignacionesVotantes
        => Set<AsignacionVotante>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }
}