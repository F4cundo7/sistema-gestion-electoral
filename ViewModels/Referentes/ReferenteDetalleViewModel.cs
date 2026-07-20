namespace SGE.ViewModels.Referentes;

public class ReferenteDetalleViewModel
{
    public int Id { get; set; }

    // Datos personales provenientes del padrón
    public long Dni { get; set; }

    public string ApellidoNombre { get; set; } = string.Empty;

    public string Localidad { get; set; } = string.Empty;

    public string Domicilio { get; set; } = string.Empty;

    public string Circuito { get; set; } = string.Empty;

    public string Escuela { get; set; } = string.Empty;

    public int Mesa { get; set; }

    // Datos propios del referente
    public string Telefono { get; set; } = string.Empty;

    public string Observaciones { get; set; } = string.Empty;

    public bool Activo { get; set; }

    // Información resumida
    public int CantidadMovilizadores { get; set; }

    public int CantidadVotantes { get; set; }

    // El referente también puede actuar como movilizador
    public bool EsMovilizador { get; set; }

    public string? TipoVehiculo { get; set; }

    public string? Patente { get; set; }

    public int CantidadVotantesPropios { get; set; }

    // Movilizadores que dependen del referente
    public List<MovilizadorResumenViewModel> Movilizadores { get; set; } = new();
}