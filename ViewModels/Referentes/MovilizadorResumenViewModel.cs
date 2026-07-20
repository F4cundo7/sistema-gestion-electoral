namespace SGE.ViewModels.Referentes;

public class MovilizadorResumenViewModel
{
    public int Id { get; set; }

    public long Dni { get; set; }

    public string ApellidoNombre { get; set; } = string.Empty;

    public string Localidad { get; set; } = string.Empty;

    public string TipoVehiculo { get; set; } = string.Empty;

    public string Patente { get; set; } = string.Empty;

    public int CantidadVotantes { get; set; }

    public bool Activo { get; set; }
}