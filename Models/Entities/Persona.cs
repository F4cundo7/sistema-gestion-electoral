using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Models.Entities;

[Table("personas")]
public class Persona
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("dni")]
    public long Dni { get; set; }

    [Required]
    [MaxLength(150)]
    [Column("apellido_nombre")]
    public string ApellidoNombre { get; set; } = string.Empty;

    [MaxLength(20)]
    [Column("sexo")]
    public string? Sexo { get; set; }

    [MaxLength(250)]
    [Column("domicilio")]
    public string? Domicilio { get; set; }

    [MaxLength(100)]
    [Column("localidad")]
    public string? Localidad { get; set; }

    [MaxLength(100)]
    [Column("departamento")]
    public string? Departamento { get; set; }

    [MaxLength(50)]
    [Column("circuito")]
    public string? Circuito { get; set; }

    [MaxLength(200)]
    [Column("escuela")]
    public string? Escuela { get; set; }

    [Column("mesa")]
    public int? Mesa { get; set; }

    [Column("orden")]
    public int? Orden { get; set; }

    public Referente? Referente { get; set; }

    public Movilizador? Movilizador { get; set; }

    public AsignacionVotante? AsignacionVotante { get; set; }

    [MaxLength(100)]
    [Column("cambio")]
    public string? Cambio { get; set; }
    [MaxLength(500)]
    [Column("observaciones")]
    public string? Observaciones { get; set; }
    
    [Column("id_seccion")]
    public int? IdSeccion { get; set; }

    [MaxLength(250)]
    [Column("domicilio_escuela")]
    public string? DomicilioEscuela { get; set; }

    [MaxLength(100)]
    [Column("localidad_escuela")]
    public string? LocalidadEscuela { get; set; }
}