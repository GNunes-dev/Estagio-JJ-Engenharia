using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Estagio.Controllers
{
    [Authorize("CookieAuth")]
    public class FuncionarioController : Controller
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

            Models.Funcionario funcionario = new Models.Funcionario();

            funcionario.Login = dados["Login"];
            funcionario.Senha = (dados["Senha"]);
            funcionario.Nome = (dados["Nome"]);
            funcionario.Email = (dados["Email"]);
            funcionario.Cpf = (dados["Cpf"]);
            funcionario.Telefone = (dados["Telefone"]);
            funcionario.Rg = (dados["Rg"]);
            funcionario.Crea = (dados["Crea"]);
            funcionario.Endereco = (dados["Endereco"]);
            funcionario.Bairro = (dados["Bairro"]);
            funcionario.Cep = (dados["Cep"]);
            funcionario.CidadeId = Convert.ToInt32((dados["Cidade"]));
            funcionario.EstadoId = Convert.ToInt32((dados["Estado"]));

            CamadaNegocio.FuncionarioCN
                    fcn = new CamadaNegocio.FuncionarioCN();
            (operacao, msg) = fcn.Criar(funcionario);


            return Json(new
            {
                operacao,
                msg
            });

        }

        public IActionResult ObterTodos()
        {
            Estagio.CamadaNegocio.FuncionarioCN ccn =
               new CamadaNegocio.FuncionarioCN();

            return Json(ccn.ObterTodos());
        }

        public IActionResult Pesquisa()
        {
            return View();
        }

        public IActionResult Pesquisar(string nome)
        {
            CamadaNegocio.FuncionarioCN fcn = new CamadaNegocio.FuncionarioCN();

            List<Models.Funcionario> funcionarios = fcn.Pesquisar(nome);

            //objeto anônimo
            var funcionarioslimpos = new List<object>();

            foreach (var u in funcionarios)
            {
                var funcionariolimpo = new
                {
                    id = u.Id,
                    nome = u.Nome,
                    login = u.Login,
                    email = u.Email,
                    cpf = u.Cpf,
                    telefone = u.Telefone,
                    rg = u.Rg,
                    crea = u.Crea,
                    endereco = u.Endereco,
                    bairro = u.Bairro,
                    cep = u.Cep,
                    cidade = u.CidadeId,
                    estado = u.EstadoId
                };

                funcionarioslimpos.Add(funcionariolimpo);
            }

            return Json(funcionarioslimpos);
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            CamadaNegocio.FuncionarioCN fcn = new CamadaNegocio.FuncionarioCN();
            bool operacao = fcn.Excluir(id);

            return Json(new
            {
                operacao
            });
        }

        public IActionResult BuscarFuncionario(int Id)
        {
            object Dado = new object();
            CamadaNegocio.FuncionarioCN fcn = new CamadaNegocio.FuncionarioCN();

            var L = fcn.BuscarFuncionario(Id);
            Dado = (new
            {
                Id = L.Id,
                Nome = L.Nome,
                Login = L.Login,
                Email = L.Email,
                Cpf = L.Cpf,
                Telefone = L.Telefone,
                Rg = L.Rg,
                Crea = L.Crea,
                Endereco = L.Endereco,
                Bairro = L.Bairro,
                Cep = L.Cep,
                Cidade = L.CidadeId,
                Estado = L.EstadoId
            });

            return Json(Dado);
        }

        [HttpPost]
        public IActionResult Editar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;

            Models.Funcionario funcionario = new Models.Funcionario();
            funcionario.Login = dados["Login"];
            funcionario.Senha = (dados["Senha"]);
            funcionario.Nome = (dados["Nome"]);
            funcionario.Email = (dados["Email"]);
            funcionario.Cpf = (dados["Cpf"]);
            funcionario.Telefone = (dados["Telefone"]);
            funcionario.Rg = (dados["Rg"]);
            funcionario.Crea = (dados["Crea"]);
            funcionario.Endereco = (dados["Endereco"]);
            funcionario.Bairro = (dados["Bairro"]);
            funcionario.Cep = (dados["Cep"]);
            funcionario.CidadeId = Convert.ToInt32((dados["Cidade"]));
            funcionario.EstadoId = Convert.ToInt32((dados["Estado"]));

            CamadaNegocio.FuncionarioCN
                    fcn = new CamadaNegocio.FuncionarioCN();
            operacao = fcn.Editar(funcionario);


            return Json(new
            {
                operacao
            });

        }
    }
}