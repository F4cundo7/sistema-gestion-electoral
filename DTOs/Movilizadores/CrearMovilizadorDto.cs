using System.ComponentModel.DataAnnotations;

namespace SGE.DTOs.Movilizadores;

public class CrearMovilizadorDto
{
    [Required]
    public long Dni { get; set; }

    [Required]
    public long DniReferente { get; set; }

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
}