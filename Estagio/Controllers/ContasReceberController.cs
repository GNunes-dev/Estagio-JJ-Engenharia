using Microsoft.AspNetCore.Mvc;

namespace Estagio.Controllers
{
    public class ContasReceberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}