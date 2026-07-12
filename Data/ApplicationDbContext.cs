using Gestor_Digital_de_Votantes.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestor_Digital_de_Votantes.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Persona> Personas => Set<Persona>();
    public DbSet<Referente> Referentes => Set<Referente>();
    public DbSet<Asignacion> Asignaciones => Set<Asignacion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Persona>()
            .Property(p => p.Dni)
            .ValueGeneratedNever();

        modelBuilder.Entity<Referente>()
            .HasIndex(r => r.PersonaDni)
            .IsUnique();

        modelBuilder.Entity<Asignacion>()
            .HasIndex(a => a.PersonaDni)
            .IsUnique();

        modelBuilder.Entity<Asignacion>()
            .Property(a => a.Rol)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}