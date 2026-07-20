namespace SGE.DTOs.Movilizadores;

public class MovilizadorDetalleDto
{
    public long Dni { get; set; }

    public string ApellidoNombre { get; set; } = string.Empty;

    public string? Sexo { get; set; }

    public string? Domicilio { get; set; }

    public string? Localidad { get; set; }

    public string? Departamento { get; set; }

    public string? Circuito { get; set; }

    public string? Escuela { get; set; }

    public int? Mesa { get; set; }

    public long DniReferente { get; set; }

    public string ReferenteApellidoNombre { get; set; } = string.Empty;

    public string Vehiculo { get; set; } = string.Empty;

    public string Patente { get; set; } = string.Empty;

    public int CantidadVotantes { get; set; }

    public DateTime FechaAlta { get; set; }

    public bool Activo { get; set; }
}