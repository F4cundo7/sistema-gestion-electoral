namespace SGE.DTOs.AsignacionesVotantes;

public class AsignacionVotanteListadoDto
{
    public long DniVotante { get; set; }

    public string VotanteApellidoNombre { get; set; } = string.Empty;

    public long DniMovilizador { get; set; }

    public string MovilizadorApellidoNombre { get; set; } = string.Empty;

    public long DniReferente { get; set; }

    public string ReferenteApellidoNombre { get; set; } = string.Empty;

    public DateTime FechaAsignacion { get; set; }

    public bool Activo { get; set; }
}