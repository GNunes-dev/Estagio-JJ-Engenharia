using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Estagio.Controllers
{
    public class RecebimentoController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult Pesquisa()
        {
            return View();
        }

        public IActionResult Quitado()
        {
            return View();
        }


        public JsonResult GravarPagamento(double valor, string data, string desc, string formapag)
        {
            string msg = "";
            bool operacao = false;

            DAL.RecebimentoDAL obd = new DAL.RecebimentoDAL();

            operacao = obd.GravarPagamento(valor, data, desc, formapag);

            return Json(new
            {
                operacao,
                msg
            });
        }

        public IActionResult ObterTodos()
        {
            DAL.RecebimentoDAL scn =
               new DAL.RecebimentoDAL();

            return Json(scn.ObterTodos());
        }

        public JsonResult Quitar(int id, double valor)
        {
            string msg = "";
            bool operacao = false;

            DAL.RecebimentoDAL obd = new DAL.RecebimentoDAL();

            operacao = obd.Quitar(id, valor);

            return Json(new
            {
                operacao,
                msg
            });
        }

        public JsonResult Extornar(int id)
        {
            string msg = "";
            bool operacao = false;

            DAL.RecebimentoDAL obd = new DAL.RecebimentoDAL();

            operacao = obd.Extornar(id);

            return Json(new
            {
                operacao,
                msg
            });
        }

        public JsonResult QuitarParcial(double valorparcial, int id)
        {
            string msg = "";
            bool operacao = false;

            DAL.RecebimentoDAL obd = new DAL.RecebimentoDAL();

            operacao = obd.QuitarParcial(valorparcial, id);

            return Json(new
            {
                operacao,
                msg
            });
        }

    }
}