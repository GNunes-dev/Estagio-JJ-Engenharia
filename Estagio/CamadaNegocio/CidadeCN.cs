using System.Collections.Generic;

namespace Estagio.CamadaNegocio
{
    public class CidadeCN
    {
        public List<Models.Estado> ObterEstados()
        {
            DAL.EstadoDAL edb = new DAL.EstadoDAL();
            return edb.ObterTodos();
        }

        public List<Models.Cidade> ObterCidades(int uf)
        {
            List<Models.Cidade> cidades = new List<Models.Cidade>();
            //ir no bd...
            DAL.CidadeDAL cbd = new DAL.CidadeDAL();
            return cbd.ObterCidades_Estado(uf);

        }
    }
}
