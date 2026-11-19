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
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.PersonaId)
            .IsUnique()
            .HasFilter("activo = true");

        builder.HasOne(x => x.Persona)
            .WithMany()
            .HasForeignKey(x => x.PersonaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Movilizador)
            .WithMany(x => x.Votantes)
            .HasForeignKey(x => x.MovilizadorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}