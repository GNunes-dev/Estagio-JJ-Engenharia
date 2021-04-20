using Estagio.Models;
using System.Collections.Generic;

namespace Estagio.CamadaNegocio
{
    public class OrgaoLicenciamentoCN
    {
        public (bool, string) Criar(Models.OrgaoLicenciamento orgao)
        {
            string msg = "";
            bool operacao = false;
            DAL.OrgaoLicenciamentoDAL obd = new DAL.OrgaoLicenciamentoDAL();

            if (obd.validarnomeUnico(orgao.Descriçao))
            {
                msg = "orgão já cadastrado.";
            }
            else
            {
                operacao = obd.Criar(orgao);
            }

            return (operacao, msg);
        }

        public Models.OrgaoLicenciamento Obter(int id)
        {
            DAL.OrgaoLicenciamentoDAL obd = new DAL.OrgaoLicenciamentoDAL();
            return obd.Obter(id);
        }

        public List<Models.OrgaoLicenciamento> ObterTodos()
        {
            DAL.OrgaoLicenciamentoDAL obd = new DAL.OrgaoLicenciamentoDAL();
            return obd.ObterTodos();
        }

        public List<Models.OrgaoLicenciamento> Pesquisar(string descricao)
        {
            DAL.OrgaoLicenciamentoDAL obd = new DAL.OrgaoLicenciamentoDAL();

            return obd.Pesquisar(descricao);
        }

        public bool Excluir(int id)
        {
            DAL.OrgaoLicenciamentoDAL obd = new DAL.OrgaoLicenciamentoDAL();
            return obd.Excluir(id);
        }

        public OrgaoLicenciamento BuscarOrgao(int id)
        {
            return new OrgaoLicenciamento().BuscarOrgao(id);
        }

        public bool Editar(OrgaoLicenciamento orgao)
        {
            bool operacao = false;
            DAL.OrgaoLicenciamentoDAL obd = new DAL.OrgaoLicenciamentoDAL();

            operacao = obd.Editar(orgao);

            return operacao;
        }
    }
}
