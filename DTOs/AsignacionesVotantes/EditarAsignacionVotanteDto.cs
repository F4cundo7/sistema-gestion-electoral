using System.ComponentModel.DataAnnotations;

namespace SGE.DTOs.AsignacionesVotantes;

public class EditarAsignacionVotanteDto
{
    [Required]
    public int Id { get; set; }
    public long DniVotante { get; set; }

    public string VotanteApellidoNombre { get; set; } = string.Empty;

    [Required]
    public long DniMovilizador { get; set; }

    public string MovilizadorApellidoNombre { get; set; } = string.Empty;

    public bool Activo { get; set; }
    
}