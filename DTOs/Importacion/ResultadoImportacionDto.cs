namespace SGE.DTOs.Importacion;

public class ResultadoImportacionDto
{
    public string NombreArchivo { get; set; } = string.Empty;

    public int FilasLeidas { get; set; }

    public int PersonasCreadas { get; set; }

    public int PersonasActualizadas { get; set; }

    public int FilasOmitidas { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public List<ErrorImportacionDto> Errores { get; set; } = [];

    public TimeSpan Duracion => FechaFin - FechaInicio;

    public bool FueExitosa => Errores.Count == 0;
}