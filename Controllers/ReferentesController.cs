using Microsoft.AspNetCore.Mvc;

namespace SGE.Controllers;

public class ReferentesController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}