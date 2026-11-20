namespace SGE.DTOs.Referentes;

public class ReferenteListadoDto
{
    public long Dni { get; set; }

    public string ApellidoNombre { get; set; } = string.Empty;

    public string? Telefono { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaAlta { get; set; }
}