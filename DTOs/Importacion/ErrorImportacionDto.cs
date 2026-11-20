namespace SGE.DTOs.Importacion;

public class ErrorImportacionDto
{
    public int NumeroFila { get; set; }

    public string? DniOriginal { get; set; }

    public string Mensaje { get; set; } = string.Empty;
}