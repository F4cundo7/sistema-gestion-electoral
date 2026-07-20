using Microsoft.AspNetCore.Mvc;
using SGE.ViewModels.Referentes;

namespace SGE.Controllers;

public class ReferentesController : Controller
{
    [HttpGet]
public IActionResult Index()
{
    var referentes = new List<ReferenteListadoViewModel>
    {
        new()
        {
            Id = 1,
            Dni = 30111222,
            ApellidoNombre = "María Acosta",
            Localidad = "San Miguel de Tucumán",
            CantidadMovilizadores = 4,
            CantidadVotantes = 24,
            Activo = true
        },

        new()
        {
            Id = 2,
            Dni = 27888999,
            ApellidoNombre = "Juan Rodríguez",
            Localidad = "Yerba Buena",
            CantidadMovilizadores = 3,
            CantidadVotantes = 16,
            Activo = true
        },

        new()
        {
            Id = 3,
            Dni = 32555666,
            ApellidoNombre = "Laura Gómez",
            Localidad = "Tafí Viejo",
            CantidadMovilizadores = 2,
            CantidadVotantes = 11,
            Activo = false
        }
    };

    return View(referentes);
}

    [HttpGet]
    public IActionResult Nuevo()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Detalle()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult Editar()
    {
        return View();
    }
}