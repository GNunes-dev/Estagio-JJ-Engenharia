using Estagio.Models;
using System.Collections.Generic;

namespace Estagio.CamadaNegocio
{
    public class SetorCN
    {
        public (bool, string) Criar(Models.Setor setor)
        {
            string msg = "";
            bool operacao = false;
            DAL.SetorDAL sbd = new DAL.SetorDAL();

            if (sbd.validarnomeUnico(setor.Descriçao))
            {
                msg = "setor já cadastrado.";
            }
            else
            {
                operacao = sbd.Criar(setor);
            }

            return (operacao, msg);
        }

        public Models.Setor Obter(int id)
        {
            DAL.SetorDAL sbd = new DAL.SetorDAL();
            return sbd.Obter(id);
        }

        public List<Models.Setor> ObterTodos()
        {
            DAL.SetorDAL sbd = new DAL.SetorDAL();
            return sbd.ObterTodos();
        }

        public List<Models.Setor> Pesquisar(string descriçao)
        {
            DAL.SetorDAL sbd = new DAL.SetorDAL();

            return sbd.Pesquisar(descriçao);
        }

        public bool Excluir(int id)
        {
            DAL.SetorDAL sbd = new DAL.SetorDAL();
            return sbd.Excluir(id);
        }

        public Setor BuscarSetor(int id)
        {
            return new Setor().BuscarSetor(id);
        }

        public bool Editar(Models.Setor setor)
        {
            bool operacao = false;
            DAL.SetorDAL sbd = new DAL.SetorDAL();

            operacao = sbd.Editar(setor);

            return operacao;
        }
    }
}
