using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estagio.CamadaNegocio
{
    public class ProjetoCN
    {
        public (bool, string, int) Criar(Models.Projeto projeto)
        {
            int id;
            string msg = "";
            bool operacao = false;
            DAL.ProjetoDAL obd = new DAL.ProjetoDAL();

            id = obd.Criar(projeto);

            if (id != -1)
                operacao = true;
            return (operacao, msg, id);
        }

        public Models.Projeto Obter(int id)
        {
            DAL.ProjetoDAL obd = new DAL.ProjetoDAL();
            return obd.Obter(id);
        }

        public List<Models.Projeto> ObterCli(int id)
        {
            DAL.ProjetoDAL sbd = new DAL.ProjetoDAL();
            return sbd.ObterCli(id);
        }

        public bool Excluir(int id)
        {
            DAL.ProjetoDAL obd = new DAL.ProjetoDAL();
            return obd.Excluir(id);
        }

        public List<Models.Projeto> ObterTodos()
        {
            DAL.ProjetoDAL sbd = new DAL.ProjetoDAL();
            return sbd.ObterTodos();
        }

        public List<Models.Serviço> BuscarProjetoServiço(int id)
        {
            DAL.ProjetoDAL sbd = new DAL.ProjetoDAL();
            return sbd.BuscarProjetoServiço(id);
        }

        public List<Models.Licença> BuscarProjetoLicença(int id)
        {
            DAL.ProjetoDAL sbd = new DAL.ProjetoDAL();
            return sbd.BuscarProjetoLicença(id);
        }

        public bool Att(int id, string status)
        {
            bool operacao = false;
            DAL.ProjetoDAL lbd = new DAL.ProjetoDAL();

            operacao = lbd.Att(id, status);

            return operacao;
        }
    }
}
