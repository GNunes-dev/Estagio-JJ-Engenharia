using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estagio.Controllers
{
    [Authorize("CookieAuth")]
    public class HomeADMController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}