using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestor_Digital_de_Votantes.Models;

[Table("personas")]
public class Persona
{
    [Key]
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

    public Asignacion? Asignacion { get; set; }
}