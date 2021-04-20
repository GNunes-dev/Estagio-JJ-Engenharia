using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Estagio.Controllers
{
    public class OrçamentoController : Controller
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

            Models.Orçamento orçamento = new Models.Orçamento();
            orçamento.clienteId = new Models.Cliente();
            orçamento.setorId = new Models.Setor();

            orçamento.Descriçao = (dados["Descriçao"]);
            orçamento.dtVencimento = (dados["dtVencimento"]);
            orçamento.clienteId.Id = Convert.ToInt32((dados["Cliente"]));
            orçamento.setorId.Id = Convert.ToInt32((dados["Setor"]));
            orçamento.FormaPag = (dados["FormaPag"]);
            orçamento.valorTotal = Convert.ToDouble((dados["ValorTotal"]));


            CamadaNegocio.OrçamentoCN
                    ocn = new CamadaNegocio.OrçamentoCN();
            (operacao, msg, id) = ocn.Criar(orçamento);


            return Json(new
            {
                operacao,
                msg,
                id
            });

        }

        public JsonResult GravarListaServiço(int id, int idorça)
        {
            string msg = "";
            bool operacao = false;

            DAL.OrçamentoDAL obd = new DAL.OrçamentoDAL();

            operacao = obd.CriarOrçamentoServiço(id, idorça);

            return Json(new
            {
                operacao,
                msg
            });
        }

        public JsonResult GravarListaLicença(int id, int idorça)
        {
            string msg = "";
            bool operacao = false;

            DAL.OrçamentoDAL obd = new DAL.OrçamentoDAL();

            operacao = obd.CriarOrçamentoLicença(id, idorça);

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
        public IActionResult Visualizar()
        {
            return View();
        }

        public IActionResult Pesquisar(string descriçao)
        {
            CamadaNegocio.OrçamentoCN ocn = new CamadaNegocio.OrçamentoCN();

            List<Models.Orçamento> orçamento = ocn.Pesquisar(descriçao);

            return Json(orçamento);
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            CamadaNegocio.OrçamentoCN ocn = new CamadaNegocio.OrçamentoCN();
            bool operacao = ocn.Excluir(id);

            return Json(new
            {
                operacao
            });
        }

        public IActionResult BuscarOrçamento(int Id)
        {
            object Dado = new object();
            CamadaNegocio.OrçamentoCN ocn = new CamadaNegocio.OrçamentoCN();

            var L = ocn.BuscarOrçamento(Id);
            if (L == null)
                return null;

            Dado = (new
            {
                Id = L.Id,
                Descriçao = L.Descriçao,
                dtVencimento = L.dtVencimento,
                clienteId = L.clienteId,
                setorId = L.setorId,
                FormaPag = L.FormaPag,
                valorTotal = L.valorTotal
            });

            return Json(Dado);
        }

        public IActionResult ObterTodos()
        {
            Estagio.CamadaNegocio.OrçamentoCN scn =
               new CamadaNegocio.OrçamentoCN();

            return Json(scn.ObterTodos());
        }

        public IActionResult BuscarOrçaCLi(int id)
        {
            Estagio.CamadaNegocio.OrçamentoCN scn =
               new CamadaNegocio.OrçamentoCN();

            return Json(scn.BuscarOrçaCli(id));
        }

        public JsonResult BuscarItensServiço(int id)
        {
            Estagio.CamadaNegocio.OrçamentoCN scn =
               new CamadaNegocio.OrçamentoCN();

            return Json(scn.BuscarItensServiço(id));
        }

        public JsonResult BuscarItensLicença(int id)
        {
            Estagio.CamadaNegocio.OrçamentoCN scn =
               new CamadaNegocio.OrçamentoCN();

            return Json(scn.BuscarItensLicença(id));
        }

        public JsonResult ExcluiOrçaLic(int id, int idlic)
        {
            Estagio.DAL.OrçamentoDAL scn =
               new DAL.OrçamentoDAL();

            return Json(scn.ExcluiOrçaLic(id, idlic));
        }

        public JsonResult ExcluiOrçaServ(int id, int idserv)
        {
            Estagio.DAL.OrçamentoDAL scn =
               new DAL.OrçamentoDAL();

            return Json(scn.ExcluiOrçaServ(id, idserv));
        }

        [HttpPost]
        public IActionResult Editar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;
            int id;
            Models.Orçamento orçamento = new Models.Orçamento();
            orçamento.clienteId = new Models.Cliente();
            orçamento.setorId = new Models.Setor();

            orçamento.Id = Convert.ToInt32(dados["Id"]);
            id = orçamento.Id;
            orçamento.Descriçao = (dados["Descriçao"]);
            orçamento.dtVencimento = (dados["dtVencimento"]);
            orçamento.clienteId.Id = Convert.ToInt32((dados["Cliente"]));
            orçamento.setorId.Id = Convert.ToInt32((dados["Setor"]));
            orçamento.FormaPag = (dados["FormaPag"]);
            orçamento.valorTotal = Convert.ToDouble(dados["ValorTotal"]);

            CamadaNegocio.OrçamentoCN
                    lcn = new CamadaNegocio.OrçamentoCN();
            operacao = lcn.Editar(orçamento);


            return Json(new
            {
                id,
                operacao
            });

        }
    }
}