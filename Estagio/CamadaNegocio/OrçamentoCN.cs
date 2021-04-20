using System.Collections.Generic;

namespace Estagio.CamadaNegocio
{
    public class OrçamentoCN
    {
        public (bool, string, int) Criar(Models.Orçamento orçamento)
        {
            int id; 
            string msg = "";
            bool operacao = false;
            DAL.OrçamentoDAL obd = new DAL.OrçamentoDAL();

            id = obd.Criar(orçamento);

            if (id != -1)
                operacao = true;
           return (operacao, msg, id);
        }

        public Models.Orçamento Obter(int id)
        {
            DAL.OrçamentoDAL obd = new DAL.OrçamentoDAL();
            return obd.Obter(id);
        }

        public List<Models.Orçamento> Pesquisar(string descriçao)
        {
            DAL.OrçamentoDAL obd = new DAL.OrçamentoDAL();

            return obd.Pesquisar(descriçao);
        }

        public bool Excluir(int id)
        {
            DAL.OrçamentoDAL obd = new DAL.OrçamentoDAL();
            return obd.Excluir(id);
        }

        public List<Models.Orçamento> ObterTodos()
        {
            DAL.OrçamentoDAL sbd = new DAL.OrçamentoDAL();
            return sbd.ObterTodos();
        }

        public List<Models.Orçamento> BuscarOrçaCli(int id)
        {
            DAL.OrçamentoDAL sbd = new DAL.OrçamentoDAL();
            return sbd.BuscarOrçaCli(id);
        }

        public List<Models.Serviço> BuscarItensServiço(int id)
        {
            DAL.OrçamentoDAL sbd = new DAL.OrçamentoDAL();
            return sbd.BuscarItensServiço(id);
        }

        public List<Models.Licença> BuscarItensLicença(int id)
        {
            DAL.OrçamentoDAL sbd = new DAL.OrçamentoDAL();
            return sbd.BuscarItensLicença(id);
        }

        public Models.Orçamento BuscarOrçamento(int id)
        {
            return new Models.Orçamento().BuscarOrçamento(id);
        }

        public bool Editar(Models.Orçamento orçamento)
        {
            bool operacao = false;
            DAL.OrçamentoDAL lbd = new DAL.OrçamentoDAL();

            operacao = lbd.Editar(orçamento);

            return operacao;
        }
    }
}
