using Estagio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Estagio.Controllers
{
    [Authorize("CookieAuth")]
    public class ServiçoController : Controller
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

            Models.Serviço serviço = new Models.Serviço();
            serviço.IdSetor = new Setor();
            serviço.Nome = (dados["Nome"]);
            serviço.Descriçao = (dados["Descriçao"]);
            serviço.Valor = Convert.ToDouble((dados["Valor"]));
            serviço.IdSetor.Id = Convert.ToInt32((dados["IdSetor"]));

            CamadaNegocio.ServiçoCN
                    scn = new CamadaNegocio.ServiçoCN();
            (operacao, msg) = scn.Criar(serviço);


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

        public IActionResult Pesquisar(string nome)
        {
            CamadaNegocio.ServiçoCN scn = new CamadaNegocio.ServiçoCN();

            List<Models.Serviço> serviço = scn.Pesquisar(nome);

            return Json(serviço);
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            CamadaNegocio.ServiçoCN scn = new CamadaNegocio.ServiçoCN();
            bool operacao = scn.Excluir(id);

            return Json(new
            {
                operacao
            });
        }

        public IActionResult BuscarServiço(int Id)
        {
            object Dado = new object();
            CamadaNegocio.ServiçoCN scn = new CamadaNegocio.ServiçoCN();

            var L = scn.BuscarServiço(Id);
            Dado = (new
            {
                Id = L.Id,
                Descriçao = L.Descriçao,
                Nome = L.Nome,
                Valor = L.Valor,
                IdSetor = L.IdSetor
            });

            return Json(Dado);
        }

        public IActionResult BuscarServiçoSetor(int id_set)
        {
            CamadaNegocio.ServiçoCN scn = new CamadaNegocio.ServiçoCN();

            return Json(scn.BuscarServiçoSetor(id_set));
        }

        public IActionResult ObterTodos()
        {
            Estagio.CamadaNegocio.ServiçoCN scn =
               new CamadaNegocio.ServiçoCN();

            return Json(scn.ObterTodos());
        }

        public IActionResult ObterValor(int serviço)
        {
            object Dado = new object();
            CamadaNegocio.ServiçoCN scn = new CamadaNegocio.ServiçoCN();

            var L = scn.BuscarServiço(serviço);
            if (L == null)
            {
                Dado = null;
            }
            else
            {
                Dado = (new
                {
                    Id = L.Id,
                    Nome = L.Nome,
                    Descriçao = L.Descriçao,
                    Valor = L.Valor,
                    IdSetor = L.IdSetor
                });
            }

            return Json(Dado);
        }


        [HttpPost]
        public IActionResult Editar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;

            Models.Serviço serviço = new Models.Serviço();
            serviço.IdSetor = new Setor();
            serviço.Id = Convert.ToInt32(dados["Id"]);
            serviço.Nome = (dados["Nome"]);
            serviço.Descriçao = (dados["Descriçao"]);
            serviço.Valor = Convert.ToDouble((dados["Valor"]));
            serviço.IdSetor.Id = Convert.ToInt32(dados["IdSetor"]);

            CamadaNegocio.ServiçoCN
                    scn = new CamadaNegocio.ServiçoCN();
            operacao = scn.Editar(serviço);


            return Json(new
            {
                operacao
            });

        }

    }
}