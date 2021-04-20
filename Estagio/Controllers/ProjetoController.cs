using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Estagio.Controllers
{
    public class ProjetoController : Controller
    {
        [Authorize("CookieAuth")]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;
            string msg = "";
            int id;
            decimal v = Convert.ToDecimal((dados["ValorTotal"]));
            Models.Projeto projeto = new Models.Projeto();
            projeto.Cliente = new Models.Cliente();
            projeto.Setor = new Models.Setor();
            projeto.Cidade = new Models.Cidade();
            projeto.Estado = new Models.Estado();
            projeto.Funcionario = new Models.Funcionario();

            projeto.Cliente.Id = Convert.ToInt32((dados["Cliente"]));
            projeto.Setor.Id = Convert.ToInt32((dados["Setor"]));
            projeto.Funcionario.Id = Convert.ToInt32((dados["Funcionario"]));
            projeto.FormaPag = (dados["FormaPag"]);
            projeto.Descriçao = (dados["Descriçao"]);
            projeto.dtInicial = (dados["dtInicial"]);
            projeto.dtPrevFinal = (dados["dtPrevFinal"]);
            projeto.dtFinal = null;
            projeto.Endereco = (dados["Endereco"]);
            projeto.Bairro = (dados["Bairro"]);
            projeto.Cep = (dados["Cep"]);
            projeto.Estado.Id = Convert.ToInt32((dados["Estado"]));
            projeto.Cidade.Id = Convert.ToInt32((dados["Cidade"]));
            projeto.valorTotal = Convert.ToDouble((dados["ValorTotal"]));

            projeto.Status = (dados["Status"]);

            CamadaNegocio.ProjetoCN
                    ocn = new CamadaNegocio.ProjetoCN();
            (operacao, msg, id) = ocn.Criar(projeto);


            return Json(new
            {
                operacao,
                msg,
                id
            });

        }

        public JsonResult GravarListaServiço(int id, int idproj)
        {
            string msg = "";
            bool operacao = false;

            DAL.ProjetoDAL obd = new DAL.ProjetoDAL();

            operacao = obd.CriarProjetoServiço(id, idproj);

            return Json(new
            {
                operacao,
                msg
            });
        }
        public JsonResult GravarListaLicença(int id, int idproj)
        {
            string msg = "";
            bool operacao = false;

            DAL.ProjetoDAL obd = new DAL.ProjetoDAL();

            operacao = obd.CriarProjetoLicença(id, idproj);

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

        public IActionResult Atualizar()
        {
            return View();
        }

        public IActionResult Visualizar()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            CamadaNegocio.ProjetoCN scn = new CamadaNegocio.ProjetoCN();
            bool operacao = scn.Excluir(id);

            return Json(new
            {
                operacao
            });
        }

        [HttpPost]
        public IActionResult Att(int id, [FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;
            string status = dados["Status"];

            CamadaNegocio.ProjetoCN
                    fcn = new CamadaNegocio.ProjetoCN();
            operacao = fcn.Att(id, status);


            return Json(new
            {
                operacao
            });

        }

        public IActionResult BuscarProjetoCli(int id)
        {
            Estagio.CamadaNegocio.ProjetoCN scn =
               new CamadaNegocio.ProjetoCN();

            return Json(scn.ObterCli(id));
        }


        public IActionResult BuscarProjeto(int id)
        {
            object Dado = new object();
            CamadaNegocio.ProjetoCN ocn = new CamadaNegocio.ProjetoCN();

            var L = ocn.Obter(id);
            Dado = (new
            {
                Id = L.Id,
                Cliente = L.Cliente,
                Setor = L.Setor,
                Funcionario = L.Funcionario,
                FormaPag = L.FormaPag,
                Descriçao = L.Descriçao,
                dtInicial = L.dtInicial,
                dtPrevFinal = L.dtPrevFinal,
                dtFinal = L.dtFinal,
                Endereco = L.Endereco,
                Bairro = L.Bairro,
                Cep = L.Cep,
                Estado = L.Estado,
                Cidade = L.Cidade,
                valorTotal = L.valorTotal,
                Status = L.Status
            });

            return Json(Dado);
        }

        public IActionResult ObterValor(int id)
        {
            object Dado = new object();
            CamadaNegocio.ProjetoCN scn = new CamadaNegocio.ProjetoCN();

            var L = scn.Obter(id);
            if (L == null)
            {
                Dado = null;
            }
            else
            {
                Dado = (new
                {
                    Id = L.Id,
                    Cliente = L.Cliente,
                    Setor = L.Setor,
                    Funcionario = L.Funcionario,
                    FormaPag = L.FormaPag,
                    Descriçao = L.Descriçao,
                    dtInicial = L.dtInicial,
                    dtPrevFinal = L.dtPrevFinal,
                    dtFinal = L.dtFinal,
                    Endereco = L.Endereco,
                    Bairro = L.Bairro,
                    Cep = L.Cep,
                    Estado = L.Estado,
                    Cidade = L.Cidade,
                    valorTotal = L.valorTotal,
                    Status = L.Status
                });
            }

            return Json(Dado);
        }

        public IActionResult ObterTodos()
        {
            Estagio.CamadaNegocio.ProjetoCN scn =
               new CamadaNegocio.ProjetoCN();

            return Json(scn.ObterTodos());
        }

        public JsonResult BuscarProjetoServiço(int id)
        {
            Estagio.CamadaNegocio.ProjetoCN scn =
               new CamadaNegocio.ProjetoCN();

            return Json(scn.BuscarProjetoServiço(id));
        }

        public JsonResult BuscarProjetoLicença(int id)
        {
            Estagio.CamadaNegocio.ProjetoCN scn =
               new CamadaNegocio.ProjetoCN();

            return Json(scn.BuscarProjetoLicença(id));
        }
    }
}