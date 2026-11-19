using System.ComponentModel.DataAnnotations;

namespace SGE.DTOs.Referentes;

public class EditarReferenteDto
{
    [Required]
    public long Dni { get; set; }

    public string ApellidoNombre { get; set; } = string.Empty;

    [StringLength(
        30,
        ErrorMessage = "El teléfono no puede superar los 30 caracteres.")]
    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    [StringLength(
        500,
        ErrorMessage = "Las observaciones no pueden superar los 500 caracteres.")]
    public string? Observaciones { get; set; }

    public bool Activo { get; set; }
}