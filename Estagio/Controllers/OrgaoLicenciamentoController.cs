using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Estagio.Controllers
{
    [Authorize("CookieAuth")]
    public class OrgaoLicenciamentoController : Controller
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

            Models.OrgaoLicenciamento orgao = new Models.OrgaoLicenciamento();
            orgao.Descriçao = (dados["Descriçao"]);
            orgao.Valor = Convert.ToDouble((dados["Valor"]));

            CamadaNegocio.OrgaoLicenciamentoCN
                    ocn = new CamadaNegocio.OrgaoLicenciamentoCN();
            (operacao, msg) = ocn.Criar(orgao);


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

        public IActionResult Pesquisar(string descricao)
        {
            CamadaNegocio.OrgaoLicenciamentoCN ocn = new CamadaNegocio.OrgaoLicenciamentoCN();

            List<Models.OrgaoLicenciamento> orgao = ocn.Pesquisar(descricao);

            return Json(orgao);
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            CamadaNegocio.OrgaoLicenciamentoCN ocn = new CamadaNegocio.OrgaoLicenciamentoCN();
            bool operacao = ocn.Excluir(id);

            return Json(new
            {
                operacao
            });
        }

        public IActionResult ObterTodos()
        {
            Estagio.CamadaNegocio.OrgaoLicenciamentoCN ocn =
               new CamadaNegocio.OrgaoLicenciamentoCN();

            return Json(ocn.ObterTodos());
        }

        public IActionResult ObterValor(int orgao)
        {
            object Dado = new object();
            CamadaNegocio.OrgaoLicenciamentoCN ocn = new CamadaNegocio.OrgaoLicenciamentoCN();

            var L = ocn.BuscarOrgao(orgao);
            if (L == null)
            {
                Dado = (new
                {
                    Id = 0,
                    Descriçao = 0,
                    Valor = 0
                });
            }
            else
            {
                Dado = (new
                {
                    Id = L.Id,
                    Descriçao = L.Descriçao,
                    Valor = L.Valor
                });
            }

            return Json(Dado);
        }

        public IActionResult BuscarOrgao(int Id)
        {
            object Dado = new object();
            CamadaNegocio.OrgaoLicenciamentoCN ocn = new CamadaNegocio.OrgaoLicenciamentoCN();

            var L = ocn.BuscarOrgao(Id);
            Dado = (new
            {
                Id = L.Id,
                Descriçao = L.Descriçao,
                Valor = L.Valor
            });

            return Json(Dado);
        }

        [HttpPost]
        public IActionResult Editar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;

            Models.OrgaoLicenciamento orgao = new Models.OrgaoLicenciamento();
            orgao.Id = Convert.ToInt32(dados["Id"]);
            orgao.Descriçao = (dados["Descriçao"]);
            orgao.Valor = Convert.ToDouble((dados["Valor"]));

            CamadaNegocio.OrgaoLicenciamentoCN
                    scn = new CamadaNegocio.OrgaoLicenciamentoCN();
            operacao = scn.Editar(orgao);


            return Json(new
            {
                operacao
            });

        }
    }
}