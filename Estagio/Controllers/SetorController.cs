using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Estagio.Controllers
{
    [Authorize("CookieAuth")]
    public class SetorController : Controller
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

            Models.Setor setor = new Models.Setor();
            setor.Descriçao = (dados["Descriçao"]);

            CamadaNegocio.SetorCN
                    scn = new CamadaNegocio.SetorCN();
            (operacao, msg) = scn.Criar(setor);


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
            CamadaNegocio.SetorCN scn = new CamadaNegocio.SetorCN();

            List<Models.Setor> setor = scn.Pesquisar(descriçao);

            return Json(setor);
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            CamadaNegocio.SetorCN scn = new CamadaNegocio.SetorCN();
            bool operacao = scn.Excluir(id);

            return Json(new
            {
                operacao
            });
        }

        public IActionResult BuscarSetor(int Id)
        {
            object Dado = new object();
            CamadaNegocio.SetorCN scn = new CamadaNegocio.SetorCN();

            var L = scn.BuscarSetor(Id);
            Dado = (new
            {
                Id = L.Id,
                Descriçao = L.Descriçao,
            });

            return Json(Dado);
        }

        public IActionResult ObterTodos()
        {
            Estagio.CamadaNegocio.SetorCN scn =
               new CamadaNegocio.SetorCN();

            return Json(scn.ObterTodos());
        }

        [HttpPost]
        public IActionResult Editar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;

            Models.Setor setor = new Models.Setor();
            setor.Id = Convert.ToInt32(dados["Id"]);
            setor.Descriçao = (dados["Descriçao"]);

            CamadaNegocio.SetorCN
                    scn = new CamadaNegocio.SetorCN();
            operacao = scn.Editar(setor);


            return Json(new
            {
                operacao
            });

        }
    }
}