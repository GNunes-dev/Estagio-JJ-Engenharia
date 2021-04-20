using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estagio.CamadaNegocio
{
    public class GerarServiçoCN
    {
        public (bool, string, int) Criar(Models.GerarServiço gerarserv)
        {
            int id;
            string msg = "";
            bool operacao = false;
            DAL.GerarServiçoDAL obd = new DAL.GerarServiçoDAL();

            id = obd.Criar(gerarserv);

            if (id != -1)
                operacao = true;
            return (operacao, msg, id);
        }

        public Models.GerarServiço Obter(int id)
        {
            DAL.GerarServiçoDAL obd = new DAL.GerarServiçoDAL();
            return obd.Obter(id);
        }

        public bool Excluir(int id)
        {
            DAL.GerarServiçoDAL obd = new DAL.GerarServiçoDAL();
            return obd.Excluir(id);
        }

        public Models.GerarServiço BuscarServiço(int id)
        {
            return new Models.GerarServiço().BuscarServiço(id);
        }

        public List<Models.GerarServiço> ObterTodos()
        {
            DAL.GerarServiçoDAL sbd = new DAL.GerarServiçoDAL();
            return sbd.ObterTodos();
        }

        public List<Models.GerarServiço> BuscarServiçoCli(int id)
        {
            DAL.GerarServiçoDAL sbd = new DAL.GerarServiçoDAL();
            return sbd.BuscarServiçoCli(id);
        }

        public List<Models.Serviço> BuscarItensServiço(int id)
        {
            DAL.GerarServiçoDAL sbd = new DAL.GerarServiçoDAL();
            return sbd.BuscarItensServiço(id);
        }

        public bool Att(int id, string status)
        {
            bool operacao = false;
            DAL.GerarServiçoDAL lbd = new DAL.GerarServiçoDAL();

            operacao = lbd.Att(id, status);

            return operacao;
        }
    }
}
