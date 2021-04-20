using Estagio.Models;
using System.Collections.Generic;

namespace Estagio.CamadaNegocio
{
    public class FuncionarioCN
    {
        public (bool, string) Criar(Models.Funcionario funcionario)
        {
            string msg = "";
            bool operacao = false;
            DAL.FuncionarioDAL fbd = new DAL.FuncionarioDAL();

            if (fbd.validarLoginUnico(funcionario.Login))
            {
                msg = "Login já cadastrado.";
            }
            else
            {
                //senha com min 6 caracteres
                if (funcionario.Senha.ToString().Length < 6)
                {
                    msg = "Senha muito pequena, deve ter no minimo 6 caracteres.";
                }
                else
                {
                    operacao = fbd.Criar(funcionario);
                }
            }

            return (operacao, msg);
        }
        public Models.Funcionario Validar(string login, string senha)
        {
            DAL.FuncionarioDAL fbd = new DAL.FuncionarioDAL();
            return fbd.Validar(login, senha);
        }

        public Models.Funcionario Obter(int id)
        {
            DAL.FuncionarioDAL fbd = new DAL.FuncionarioDAL();
            return fbd.Obter(id);
        }

        public List<Models.Funcionario> ObterTodos()
        {
            DAL.FuncionarioDAL cbd = new DAL.FuncionarioDAL();
            return cbd.ObterTodos();
        }

        public List<Models.Funcionario> Pesquisar(string nome)
        {
            DAL.FuncionarioDAL fbd = new DAL.FuncionarioDAL();

            return fbd.Pesquisar(nome);
        }

        public bool Excluir(int id)
        {
            DAL.FuncionarioDAL fbd = new DAL.FuncionarioDAL();
            return fbd.Excluir(id);
        }

        public Funcionario BuscarFuncionario(int id)
        {
            return new Funcionario().BuscarFuncionario(id);
        }

        public bool Editar(Models.Funcionario funcionario)
        {
            bool operacao = false;
            DAL.FuncionarioDAL fbd = new DAL.FuncionarioDAL();

            operacao = fbd.Editar(funcionario);

            return operacao;
        }
    }
}
