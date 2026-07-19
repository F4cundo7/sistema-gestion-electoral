using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SGE.Models.Entities;

[Table("referentes")]
public class Referente
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("persona_id")]
    public int PersonaId { get; set; }

    [ForeignKey(nameof(PersonaId))]
    public Persona Persona { get; set; } = null!;

    [MaxLength(30)]
    [Column("telefono")]
    public string? Telefono { get; set; }

    [MaxLength(500)]
    [Column("observaciones")]
    public string? Observaciones { get; set; }

    [Column("fecha_alta")]
    public DateTime FechaAlta { get; set; } = DateTime.UtcNow;

   [Column("activo")]
    public bool Activo { get; set; } = true;

    public ICollection<Movilizador> Movilizadores { get; set; }
        = new List<Movilizador>();
}
    