using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace Estagio.Controllers
{
    [Authorize("CookieAuth")]
    public class LoginAdmController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }



        [AllowAnonymous]
        [HttpPost]
        public IActionResult Logar([FromBody] Dictionary<string, string> dados)
        {
            string login = dados["login"];
            string senha = dados["senha"];
            CamadaNegocio.FuncionarioCN fcn = new CamadaNegocio.FuncionarioCN();

            Models.Funcionario usuarioOk = fcn.Validar(login, senha);

            if (usuarioOk != null)
            {
                #region Criando as cookie de autenticação

                var funcionarioClaims = new List<Claim>()
                {
                    new Claim("Id", usuarioOk.Id.ToString()),
                    new Claim("Login", usuarioOk.Login.ToString())
                };

                var identificacao = new ClaimsIdentity(funcionarioClaims, "Identificação do Usuário");
                var principal = new ClaimsPrincipal(identificacao);

                //gerar a cookie
                Microsoft.AspNetCore
                    .Authentication
                    .AuthenticationHttpContextExtensions
                    .SignInAsync(HttpContext, principal);


                #endregion
                return Json(new
                {
                    operacao = true,
                });
            }
            else
            {
                return Json(new
                {
                    operacao = false,
                    msg = "Dados inválidos"
                });

            }
        }

        public IActionResult Sair()
        {
            //excluíndo  a cookie
            Microsoft.AspNetCore
                .Authentication
                .AuthenticationHttpContextExtensions
                .SignOutAsync(HttpContext);

            return Redirect("/LoginAdm/Index");

        }
    }
}