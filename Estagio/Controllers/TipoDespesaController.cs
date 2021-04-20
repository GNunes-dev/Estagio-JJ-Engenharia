using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Estagio.Controllers
{
    [Authorize("CookieAuth")]
    public class TipoDespesaController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;
            string msg = "";

            Models.TipoDespesa tipoDespesa = new Models.TipoDespesa();
            tipoDespesa.Descriçao = (dados["Descriçao"]);

            CamadaNegocio.TipoDespesaCN
                    tcn = new CamadaNegocio.TipoDespesaCN();
            (operacao, msg) = tcn.Criar(tipoDespesa);


            return Json(new
            {
                operacao,
                msg
            });

        }

        public IActionResult Pesquisa()
        {
            return View();
        }

        public IActionResult Pesquisar(string descriçao)
        {
            CamadaNegocio.TipoDespesaCN tcn = new CamadaNegocio.TipoDespesaCN();

            List<Models.TipoDespesa> tipoDespesa = tcn.Pesquisar(descriçao);

            return Json(tipoDespesa);
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            CamadaNegocio.TipoDespesaCN tcn = new CamadaNegocio.TipoDespesaCN();
            bool operacao = tcn.Excluir(id);

            return Json(new
            {
                operacao
            });
        }

        public IActionResult BuscarTipoDespesa(int Id)
        {
            object Dado = new object();
            CamadaNegocio.TipoDespesaCN tcn = new CamadaNegocio.TipoDespesaCN();
            var L = tcn.BuscarTipoDespesa(Id);
            Dado = (new
            {
                Id = L.Id,
                Descriçao = L.Descriçao,
            });

            return Json(Dado);
        }

        public IActionResult ObterTodos()
        {
            Estagio.CamadaNegocio.TipoDespesaCN scn =
               new CamadaNegocio.TipoDespesaCN();

            return Json(scn.ObterTodos());
        }

        [HttpPost]
        public IActionResult Editar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;

            Models.TipoDespesa tipoDespesa = new Models.TipoDespesa();
            tipoDespesa.Id = Convert.ToInt32(dados["Id"]);
            tipoDespesa.Descriçao = (dados["Descriçao"]);

            CamadaNegocio.TipoDespesaCN
                    tcn = new CamadaNegocio.TipoDespesaCN();
            operacao = tcn.Editar(tipoDespesa);


            return Json(new
            {
                operacao
            });

        }
    }
}