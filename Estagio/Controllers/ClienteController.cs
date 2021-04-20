using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Estagio.Controllers
{
    [Authorize("CookieAuth")]
    public class ClienteController : Controller
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

            Models.Cliente cliente = new Models.Cliente();
            cliente.Login = dados["Login"];
            cliente.Senha = (dados["Senha"]);
            cliente.Nome = (dados["Nome"]);
            cliente.Email = (dados["Email"]);
            cliente.Cpf = (dados["Cpf"]);
            cliente.Telefone = (dados["Telefone"]);
            cliente.Rg = (dados["Rg"]);
            cliente.Endereco = (dados["Endereco"]);
            cliente.Bairro = (dados["Bairro"]);
            cliente.Cep = (dados["Cep"]);
            cliente.CidadeId = Convert.ToInt32((dados["Cidade"]));
            cliente.EstadoId = Convert.ToInt32((dados["Estado"]));

            CamadaNegocio.ClienteCN
                    ccn = new CamadaNegocio.ClienteCN();
            (operacao, msg) = ccn.Criar(cliente);


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
            CamadaNegocio.ClienteCN ccn = new CamadaNegocio.ClienteCN();

            List<Models.Cliente> cliente = ccn.Pesquisar(nome);

            //objeto anônimo
            var clienteslimpos = new List<object>();

            foreach (var u in cliente)
            {
                var clientelimpo = new
                {
                    id = u.Id,
                    nome = u.Nome,
                    login = u.Login,
                    email = u.Email,
                    cpf = u.Cpf,
                    telefone = u.Telefone,
                    rg = u.Rg,
                    endereco = u.Endereco,
                    bairro = u.Bairro,
                    cep = u.Cep,
                    cidade = u.CidadeId,
                    estado = u.EstadoId
                };

                clienteslimpos.Add(clientelimpo);
            }

            return Json(clienteslimpos);
        }

        public IActionResult ObterTodos()
        {
            Estagio.CamadaNegocio.ClienteCN ccn =
               new CamadaNegocio.ClienteCN();

            return Json(ccn.ObterTodos());
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            CamadaNegocio.ClienteCN ccn = new CamadaNegocio.ClienteCN();
            bool operacao = ccn.Excluir(id);

            return Json(new
            {
                operacao
            });
        }

        public IActionResult BuscarCliente(int Id)
        {
            object Dado = new object();
            CamadaNegocio.ClienteCN ccn = new CamadaNegocio.ClienteCN();

            var L = ccn.BuscarCliente(Id);
            Dado = (new
            {
                Id = L.Id,
                Nome = L.Nome,
                Login = L.Login,
                Email = L.Email,
                Cpf = L.Cpf,
                Telefone = L.Telefone,
                Rg = L.Rg,
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

            Models.Cliente cliente = new Models.Cliente();
            cliente.Login = dados["Login"];
            cliente.Senha = (dados["Senha"]);
            cliente.Nome = (dados["Nome"]);
            cliente.Email = (dados["Email"]);
            cliente.Cpf = (dados["Cpf"]);
            cliente.Telefone = (dados["Telefone"]);
            cliente.Rg = (dados["Rg"]);
            cliente.Endereco = (dados["Endereco"]);
            cliente.Bairro = (dados["Bairro"]);
            cliente.Cep = (dados["Cep"]);
            cliente.CidadeId = Convert.ToInt32((dados["Cidade"]));
            cliente.EstadoId = Convert.ToInt32((dados["Estado"]));

            CamadaNegocio.ClienteCN
                    ccn = new CamadaNegocio.ClienteCN();
            operacao = ccn.Editar(cliente);


            return Json(new
            {
                operacao
            });

        }

        [HttpPost]
        public IActionResult EditarCli([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;

            Models.Cliente cliente = new Models.Cliente();
            cliente.Login = dados["Login"];
            cliente.Senha = (dados["Senha"]);
            cliente.Nome = (dados["Nome"]);
            cliente.Email = (dados["Email"]);
            cliente.Cpf = (dados["Cpf"]);
            cliente.Telefone = (dados["Telefone"]);
            cliente.Rg = (dados["Rg"]);
            cliente.Endereco = (dados["Endereco"]);
            cliente.Bairro = (dados["Bairro"]);
            cliente.Cep = (dados["Cep"]);
            cliente.CidadeId = Convert.ToInt32((dados["Cidade"]));
            cliente.EstadoId = Convert.ToInt32((dados["Estado"]));

            CamadaNegocio.ClienteCN
                    ccn = new CamadaNegocio.ClienteCN();
            operacao = ccn.Editar(cliente);


            return Json(new
            {
                operacao
            });

        }
    }
}