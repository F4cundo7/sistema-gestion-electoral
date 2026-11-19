using System.ComponentModel.DataAnnotations;

namespace SGE.DTOs.AsignacionesVotantes;

public class CrearAsignacionVotanteDto
{
    [Required]
    public long DniVotante { get; set; }

    [Required]
    public long DniMovilizador { get; set; }
}