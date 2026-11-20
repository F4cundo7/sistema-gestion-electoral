using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Models.Entities;

[Table("asignaciones_votantes")]
public class AsignacionVotante
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("persona_id")]
    public int PersonaId { get; set; }

    [ForeignKey(nameof(PersonaId))]
    public Persona Persona { get; set; } = null!;

    [Required]
    [Column("movilizador_id")]
    public int MovilizadorId { get; set; }

    [ForeignKey(nameof(MovilizadorId))]
    public Movilizador Movilizador { get; set; } = null!;

    [Column("fecha_asignacion")]
    public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;

    [Column("activo")]
    public bool Activo { get; set; } = true;
}