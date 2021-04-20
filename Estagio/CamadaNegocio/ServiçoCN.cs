using Estagio.Models;
using System.Collections.Generic;

namespace Estagio.CamadaNegocio
{
    public class ServiçoCN
    {
        public (bool, string) Criar(Models.Serviço serviço)
        {
            string msg = "";
            bool operacao = false;
            DAL.ServiçoDAL sbd = new DAL.ServiçoDAL();

            if (sbd.validarnomeUnico(serviço.Nome))
            {
                msg = "serviço já cadastrado.";
            }
            else
            {
                operacao = sbd.Criar(serviço);
            }

            return (operacao, msg);
        }

        public Models.Serviço Obter(int id)
        {
            DAL.ServiçoDAL sbd = new DAL.ServiçoDAL();
            return sbd.Obter(id);
        }

        public List<Models.Serviço> Pesquisar(string nome)
        {
            DAL.ServiçoDAL sbd = new DAL.ServiçoDAL();

            return sbd.Pesquisar(nome);
        }

        public bool Excluir(int id)
        {
            DAL.ServiçoDAL sbd = new DAL.ServiçoDAL();
            return sbd.Excluir(id);
        }

        public List<Models.Serviço> ObterTodos()
        {
            DAL.ServiçoDAL sbd = new DAL.ServiçoDAL();
            return sbd.ObterTodos();
        }

        public Serviço BuscarServiço(int id)
        {
            return new Serviço().BuscarServiço(id);
        }

        public List<Models.Serviço> BuscarServiçoSetor(int idsetor)
        {
            DAL.ServiçoDAL sbd = new DAL.ServiçoDAL();

            if (idsetor > 0)
                return new DAL.ServiçoDAL().BuscarServiçoSetor(idsetor);
            else
                return null;
        }

        public bool Editar(Models.Serviço serviço)
        {
            bool operacao = false;
            DAL.ServiçoDAL sbd = new DAL.ServiçoDAL();

            operacao = sbd.Editar(serviço);

            return operacao;
        }
    }
}
