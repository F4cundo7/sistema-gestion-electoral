using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGE.Models.Entities;

namespace SGE.Data.Configurations;

public class PersonaConfiguration
    : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.HasIndex(persona => persona.Dni)
            .IsUnique();
    }
}