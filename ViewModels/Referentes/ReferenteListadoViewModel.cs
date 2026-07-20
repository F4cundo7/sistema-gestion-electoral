namespace SGE.ViewModels.Referentes;

public class ReferenteListadoViewModel
{
    public int Id { get; set; }

    public long Dni { get; set; }

    public string ApellidoNombre { get; set; } = string.Empty;

    public string Localidad { get; set; } = string.Empty;

    public int CantidadMovilizadores { get; set; }

    public int CantidadVotantes { get; set; }

    public bool Activo { get; set; }
}