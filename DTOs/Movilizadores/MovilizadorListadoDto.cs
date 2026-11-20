namespace SGE.DTOs.Movilizadores;

public class MovilizadorListadoDto
{
    public long Dni { get; set; }

    public string ApellidoNombre { get; set; } = string.Empty;

    public long DniReferente { get; set; }

    public string ReferenteApellidoNombre { get; set; } = string.Empty;

    public string Vehiculo { get; set; } = string.Empty;

    public string Patente { get; set; } = string.Empty;

    public int CantidadVotantes { get; set; }

    public DateTime FechaAlta { get; set; }

    public bool Activo { get; set; }
}