using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGE.Models.Entities;

namespace SGE.Data.Configurations;

public class ReferenteConfiguration
    : IEntityTypeConfiguration<Referente>
{
    public void Configure(EntityTypeBuilder<Referente> builder)
    {
        builder.HasIndex(referente => referente.PersonaId)
            .IsUnique();

        builder.HasOne(referente => referente.Persona)
            .WithOne(persona => persona.Referente)
            .HasForeignKey<Referente>(
                referente => referente.PersonaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}