using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Models;

[Table("asignaciones")]
public class Asignacion
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("referente_id")]
    public int ReferenteId { get; set; }

    [ForeignKey(nameof(ReferenteId))]
    public Referente Referente { get; set; } = null!;

    [Required]
    [Column("persona_id")]
    public int PersonaId { get; set; }

    [ForeignKey(nameof(PersonaId))]
    public Persona Persona { get; set; } = null!;

    [Required]
    [Column("rol")]
    public RolAsignacion Rol { get; set; }

    [MaxLength(100)]
    [Column("vehiculo")]
    public string? Vehiculo { get; set; }

    [MaxLength(15)]
    [Column("patente")]
    public string? Patente { get; set; }

    [Column("fecha_asignacion")]
    public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;

    [Column("activo")]
    public bool Activo { get; set; } = true;
}