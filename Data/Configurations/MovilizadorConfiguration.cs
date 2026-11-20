using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGE.Models.Entities;

namespace SGE.Data.Configurations;

public class MovilizadorConfiguration
    : IEntityTypeConfiguration<Movilizador>
{
    public void Configure(EntityTypeBuilder<Movilizador> builder)
    {
        builder.HasIndex(movilizador => movilizador.PersonaId)
            .IsUnique();

        builder.HasOne(movilizador => movilizador.Persona)
            .WithOne(persona => persona.Movilizador)
            .HasForeignKey<Movilizador>(
                movilizador => movilizador.PersonaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(movilizador => movilizador.Referente)
            .WithMany(referente => referente.Movilizadores)
            .HasForeignKey(movilizador => movilizador.ReferenteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}