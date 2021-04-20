using Estagio.Models;
using System.Collections.Generic;

namespace Estagio.CamadaNegocio
{
    public class ClienteCN
    {
        public (bool, string) Criar(Models.Cliente cliente)
        {
            string msg = "";
            bool operacao = false;
            DAL.ClienteDAL cbd = new DAL.ClienteDAL();

            if (cbd.validarLoginUnico(cliente.Login))
            {
                msg = "Login já cadastrado.";
            }
            else
            {
                //senha com min 6 caracteres
                if (cliente.Senha.ToString().Length < 6)
                {
                    msg = "Senha muito pequena, deve ter no minimo 6 caracteres.";
                }
                else
                {
                    operacao = cbd.Criar(cliente);
                }
            }

            return (operacao, msg);
        }
        public Models.Cliente Validar(string login, string senha)
        {
            DAL.ClienteDAL cbd = new DAL.ClienteDAL();
            return cbd.Validar(login, senha);
        }

        public Models.Cliente Obter(int id)
        {
            DAL.ClienteDAL cbd = new DAL.ClienteDAL();
            return cbd.Obter(id);
        }

        public List<Models.Cliente> ObterTodos()
        {
            DAL.ClienteDAL cbd = new DAL.ClienteDAL();
            return cbd.ObterTodos();
        }

        public List<Models.Cliente> Pesquisar(string nome)
        {
            DAL.ClienteDAL cbd = new DAL.ClienteDAL();

            return cbd.Pesquisar(nome);
        }

        public bool Excluir(int id)
        {
            DAL.ClienteDAL cbd = new DAL.ClienteDAL();
            return cbd.Excluir(id);
        }

        public Cliente BuscarCliente(int id)
        {
            return new Cliente().BuscarCliente(id);
        }

        public bool Editar(Models.Cliente cliente)
        {
            bool operacao = false;
            DAL.ClienteDAL cbd = new DAL.ClienteDAL();

            operacao = cbd.Editar(cliente);

            return operacao;
        }

        public bool EditarCli(Models.Cliente cliente)
        {
            bool operacao = false;
            DAL.ClienteDAL cbd = new DAL.ClienteDAL();

            operacao = cbd.EditarCli(cliente);

            return operacao;
        }
    }
}
