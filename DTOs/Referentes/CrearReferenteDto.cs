using System.ComponentModel.DataAnnotations;

namespace SGE.DTOs.Referentes;

public class CrearReferenteDto
{
    [Required(ErrorMessage = "Debe ingresar el DNI.")]
    [Range(
        1_000_000,
        99_999_999,
        ErrorMessage = "El DNI debe tener entre 7 y 8 dígitos.")]
    [Display(Name = "DNI")]
    public long Dni { get; set; }

    [StringLength(
        30,
        ErrorMessage = "El teléfono no puede superar los 30 caracteres.")]
    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    [StringLength(
        500,
        ErrorMessage = "Las observaciones no pueden superar los 500 caracteres.")]
    public string? Observaciones { get; set; }
}