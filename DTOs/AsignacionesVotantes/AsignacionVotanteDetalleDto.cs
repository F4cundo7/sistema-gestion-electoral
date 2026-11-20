namespace SGE.DTOs.AsignacionesVotantes;

public class AsignacionVotanteDetalleDto
{
    public int Id { get; set; }
    public long DniVotante { get; set; }

    public string VotanteApellidoNombre { get; set; } = string.Empty;

    public string? Sexo { get; set; }

    public string? Domicilio { get; set; }

    public string? Localidad { get; set; }

    public string? Departamento { get; set; }

    public string? Circuito { get; set; }

    public string? Escuela { get; set; }

    public int? Mesa { get; set; }

    public long DniMovilizador { get; set; }

    public string MovilizadorApellidoNombre { get; set; } = string.Empty;

    public long DniReferente { get; set; }

    public string ReferenteApellidoNombre { get; set; } = string.Empty;

    public DateTime FechaAsignacion { get; set; }

    public bool Activo { get; set; }
}