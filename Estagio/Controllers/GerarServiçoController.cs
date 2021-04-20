using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Estagio.Controllers
{
    public class GerarServiçoController : Controller
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

            Models.GerarServiço gerarserv = new Models.GerarServiço();
            gerarserv.clienteId = new Models.Cliente();

            gerarserv.clienteId.Id = Convert.ToInt32((dados["Cliente"]));
            gerarserv.funcionarioiId = Convert.ToInt32((dados["Funcionario"]));
            gerarserv.setorId = Convert.ToInt32((dados["Setor"]));
            gerarserv.Descriçao = (dados["Descriçao"]);
            gerarserv.dtInicio = (dados["dtInicial"]);
            gerarserv.dtPrevFim = (dados["dtPrevFinal"]);
            gerarserv.dtFim = null;
            gerarserv.Endereco = (dados["Endereco"]);
            gerarserv.Bairro = (dados["Bairro"]);
            gerarserv.Cep = (dados["Cep"]);
            gerarserv.estadoId = Convert.ToInt32((dados["Estado"]));
            gerarserv.cidadeId = Convert.ToInt32((dados["Cidade"]));
            gerarserv.ValorTotal = Convert.ToDouble((dados["ValorTotal"]));
            gerarserv.Status = (dados["Status"]);

            CamadaNegocio.GerarServiçoCN
                    ocn = new CamadaNegocio.GerarServiçoCN();
            (operacao, msg, id) = ocn.Criar(gerarserv);


            return Json(new
            {
                operacao,
                msg,
                id
            });

        }

        public JsonResult GravarListaServiço(int id_servico, int id_gerarserv)
        {
            string msg = "";
            bool operacao = false;

            DAL.GerarServiçoDAL obd = new DAL.GerarServiçoDAL();

            operacao = obd.CriarItensServiço(id_servico, id_gerarserv);

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
            CamadaNegocio.GerarServiçoCN scn = new CamadaNegocio.GerarServiçoCN();
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
            
            CamadaNegocio.GerarServiçoCN
                    fcn = new CamadaNegocio.GerarServiçoCN();
            operacao = fcn.Att(id, status);


            return Json(new
            {
                operacao
            });

        }

        public IActionResult BuscarServiço(int id)
        {
            object Dado = new object();
            CamadaNegocio.GerarServiçoCN ocn = new CamadaNegocio.GerarServiçoCN();

            var L = ocn.BuscarServiço(id);
            Dado = (new
            {
                Id = L.Id,
                clienteId = L.clienteId,
                funcionarioId = L.funcionarioiId,
                setorId = L.setorId,
                Descriçao = L.Descriçao,
                dtInicial = L.dtInicio,
                dtPrevFinal = L.dtPrevFim,
                Endereco = L.Endereco,
                Bairro = L.Bairro,
                Cep = L.Cep,
                Estado = L.estadoId,
                Cidade = L.cidadeId,
                valorTotal = L.ValorTotal,
                Status = L.Status
            });

            return Json(Dado);
        }

        public IActionResult ObterValor(int id)
        {
            object Dado = new object();
            CamadaNegocio.GerarServiçoCN scn = new CamadaNegocio.GerarServiçoCN();

            var L = scn.BuscarServiço(id);
            if (L == null)
            {
                Dado = (new
                {
                    Id = 0,
                    clienteId = -1,
                    funcionarioId = -1,
                    setorId = -1,
                    Descriçao = "",
                    dtInicial = "",
                    dtPrevFinal = "",
                    Endereco = "",
                    Bairro = "",
                    Cep = "",
                    Estado = -1,
                    Cidade = -1,
                    valorTotal = 0,
                    Status = ""
                }); ;
            }
            else
            {
                Dado = (new
                {
                    Id = L.Id,
                    clienteId = L.clienteId,
                    funcionarioId = L.funcionarioiId,
                    setorId = L.setorId,
                    Descriçao = L.Descriçao,
                    dtInicial = L.dtInicio,
                    dtPrevFinal = L.dtPrevFim,
                    Endereco = L.Endereco,
                    Bairro = L.Bairro,
                    Cep = L.Cep,
                    Estado = L.estadoId,
                    Cidade = L.cidadeId,
                    valorTotal = L.ValorTotal,
                    Status = L.Status
                });
            }

            return Json(Dado);
        }

        public IActionResult BuscarServiçoCli(int id)
        {
            Estagio.CamadaNegocio.GerarServiçoCN scn =
               new CamadaNegocio.GerarServiçoCN();

            return Json(scn.BuscarServiçoCli(id));
        }
        public IActionResult ObterTodos()
        {
            Estagio.CamadaNegocio.GerarServiçoCN scn =
               new CamadaNegocio.GerarServiçoCN();

            return Json(scn.ObterTodos());
        }

        public JsonResult BuscarItensServiço(int id)
        {
            Estagio.CamadaNegocio.GerarServiçoCN scn =
               new CamadaNegocio.GerarServiçoCN();

            return Json(scn.BuscarItensServiço(id));
        }

    }
}