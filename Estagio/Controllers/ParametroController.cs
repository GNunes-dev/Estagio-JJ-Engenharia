using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Estagio.Controllers
{
    public class ParametroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public bool obterParametro()
        {
            DAL.ParametroDAL fbd = new DAL.ParametroDAL();

            bool operacao = fbd.obterParametro();

            return operacao;
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;
            string msg = "";

            Models.Parametro parametro = new Models.Parametro();

            parametro.Razao = (dados["Razao"]);
            parametro.NomeFantasia = (dados["NomeFantasia"]);
            parametro.Cnpj = (dados["CNPJ"]);
            parametro.Ie = (dados["IE"]);
            parametro.DataInicio = (dados["DataInicio"]);
            parametro.Endereco = (dados["Endereco"]);
            parametro.Bairro = (dados["Bairro"]);
            parametro.Cep = (dados["Cep"]);
            parametro.CidadeId = Convert.ToInt32((dados["Cidade"]));
            parametro.EstadoId = Convert.ToInt32((dados["Estado"]));
            parametro.Email = (dados["Email"]).ToString();
            parametro.Telefone = (dados["Telefone"]);
            CamadaNegocio.ParametroCN pcn = new CamadaNegocio.ParametroCN();

            (operacao, msg) = pcn.Criar(parametro);

            return Json(new
            {
                operacao,
                msg
            });

        }
    }
}