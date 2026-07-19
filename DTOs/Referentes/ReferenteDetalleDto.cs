namespace SGE.DTOs.Referentes;

public class ReferenteDetalleDto
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

    public string? Telefono { get; set; }

    public string? Observaciones { get; set; }

    public DateTime FechaAlta { get; set; }

    public bool Activo { get; set; }

    public int CantidadMovilizadores { get; set; }
}