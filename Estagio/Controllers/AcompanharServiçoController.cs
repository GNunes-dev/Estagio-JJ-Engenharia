using Microsoft.AspNetCore.Mvc;

namespace Estagio.Controllers
{
    public class AcompanharServiçoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Visualizar()
        {
            return View();
        }
    }
}