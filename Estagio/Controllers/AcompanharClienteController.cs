using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Estagio.Controllers
{
    public class AcompanharClienteController : Controller
    {
        public IActionResult Visualizar()
        {
            return View();
        }
    }
}