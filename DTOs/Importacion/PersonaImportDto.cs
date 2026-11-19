namespace SGE.DTOs.Importacion;

public class PersonaImportDto
{
    public long Dni { get; set; }

    public string ApellidoNombre { get; set; } = string.Empty;

    public string? Sexo { get; set; }

    public string? Domicilio { get; set; }

    public string? Circuito { get; set; }

    public string? Localidad { get; set; }

    public string? Departamento { get; set; }

    public int? IdSeccion { get; set; }

    public string? Escuela { get; set; }

    public int? Mesa { get; set; }

    public string? DomicilioEscuela { get; set; }

    public string? LocalidadEscuela { get; set; }

    public int? Orden { get; set; }

    public string? Cambio { get; set; }

    public string? Observaciones { get; set; }
}