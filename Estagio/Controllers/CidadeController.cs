using Microsoft.AspNetCore.Mvc;

namespace Estagio.Controllers
{
    public class CidadeController : Controller
    {
        public IActionResult ObterEstados()
        {
            Estagio.CamadaNegocio.CidadeCN ccn =
               new CamadaNegocio.CidadeCN();

            return Json(ccn.ObterEstados());


        }

        public IActionResult GetEstado(int id)
        {
            DAL.EstadoDAL ecn =
               new DAL.EstadoDAL();

            return Json(ecn.GetEstado(id));


        }

        public IActionResult GetCidade(int id)
        {
            DAL.CidadeDAL ecn =
               new DAL.CidadeDAL();

            return Json(ecn.GetCidade(id));


        }

        public IActionResult ObterCidades(int uf)
        {
            Estagio.CamadaNegocio.CidadeCN ccn =
                new CamadaNegocio.CidadeCN();

            return Json(ccn.ObterCidades(uf));

        }

    }
}