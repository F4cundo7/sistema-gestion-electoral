using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGE.Models.Entities;

namespace SGE.Data.Configurations;

public class AsignacionVotanteConfiguration
    : IEntityTypeConfiguration<AsignacionVotante>
{
    public void Configure(
        EntityTypeBuilder<AsignacionVotante> builder)
    {
        builder.HasIndex(asignacion => asignacion.PersonaId)
            .IsUnique();

        builder.HasOne(asignacion => asignacion.Persona)
            .WithOne(persona => persona.AsignacionVotante)
            .HasForeignKey<AsignacionVotante>(
                asignacion => asignacion.PersonaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(asignacion => asignacion.Movilizador)
            .WithMany(movilizador => movilizador.Votantes)
            .HasForeignKey(asignacion => asignacion.MovilizadorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}