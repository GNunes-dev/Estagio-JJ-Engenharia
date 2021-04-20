using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Estagio.Controllers
{
    [Authorize("CookieAuth")]
    public class HomeCliController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public int pegaId()
        {
            object Dado = new object();

            var user = (ClaimsIdentity)User.Identity;
            int id = Convert.ToInt32(user.FindFirst("Id").Value);

            return id;
        }
    }
}