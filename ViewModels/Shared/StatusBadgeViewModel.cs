namespace SGE.ViewModels.Shared;

public class StatusBadgeViewModel
{
    public bool Activo { get; set; }

    public string TextoActivo { get; set; } = "Activo";

    public string TextoInactivo { get; set; } = "Inactivo";
}