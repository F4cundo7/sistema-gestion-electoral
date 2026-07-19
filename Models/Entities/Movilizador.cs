using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Models.Entities;

[Table("movilizadores")]
public class Movilizador
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
    [Column("referente_id")]
    public int ReferenteId { get; set; }

    [ForeignKey(nameof(ReferenteId))]
    public Referente Referente { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    [Column("vehiculo")]
    public string Vehiculo { get; set; } = string.Empty;

    [Required]
    [MaxLength(15)]
    [Column("patente")]
    public string Patente { get; set; } = string.Empty;

    [Column("fecha_alta")]
    public DateTime FechaAlta { get; set; } = DateTime.UtcNow;

    [Column("activo")]
    public bool Activo { get; set; } = true;

    public ICollection<AsignacionVotante> Votantes { get; set; }
        = new List<AsignacionVotante>();
}