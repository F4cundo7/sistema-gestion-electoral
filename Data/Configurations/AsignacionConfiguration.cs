using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGE.Models;

namespace SGE.Data.Configurations;

public class AsignacionConfiguration
    : IEntityTypeConfiguration<Asignacion>
{
    public void Configure(EntityTypeBuilder<Asignacion> builder)
    {
        builder.HasIndex(asignacion => asignacion.PersonaId)
            .IsUnique();

        builder.HasOne(asignacion => asignacion.Persona)
            .WithOne(persona => persona.Asignacion)
            .HasForeignKey<Asignacion>(
                asignacion => asignacion.PersonaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(asignacion => asignacion.Referente)
            .WithMany(referente => referente.Asignaciones)
            .HasForeignKey(asignacion => asignacion.ReferenteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(asignacion => asignacion.Rol)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.ToTable(
            "asignaciones",
            tabla => tabla.HasCheckConstraint(
                "CK_asignaciones_datos_movilizador",
                """
                (
                    rol = 'Movilizador'
                    AND vehiculo IS NOT NULL
                    AND patente IS NOT NULL
                )
                OR
                (
                    rol = 'Votante'
                    AND vehiculo IS NULL
                    AND patente IS NULL
                )
                """));
    }
}