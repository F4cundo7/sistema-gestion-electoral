using System.ComponentModel.DataAnnotations;

namespace SGE.DTOs.Movilizadores;

public class EditarMovilizadorDto
{
    [Required]
    public long Dni { get; set; }

    public string ApellidoNombre { get; set; } = string.Empty;

    public long DniReferente { get; set; }

    public string ReferenteApellidoNombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe ingresar el vehículo.")]
    [StringLength(
        100,
        ErrorMessage = "El vehículo no puede superar los 100 caracteres.")]
    public string Vehiculo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe ingresar la patente.")]
    [StringLength(
        15,
        ErrorMessage = "La patente no puede superar los 15 caracteres.")]
    public string Patente { get; set; } = string.Empty;

    public bool Activo { get; set; }
}