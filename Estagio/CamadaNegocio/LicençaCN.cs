using Estagio.Models;
using System.Collections.Generic;

namespace Estagio.CamadaNegocio
{
    public class LicençaCN
    {
        public (bool, string) Criar(Models.Licença licença)
        {
            string msg = "";
            bool operacao = false;
            DAL.LicençaDAL lbd = new DAL.LicençaDAL();

            operacao = lbd.Criar(licença);

            return (operacao, msg);
        }

        public Models.Licença Obter(int id)
        {
            DAL.LicençaDAL lbd = new DAL.LicençaDAL();
            return lbd.Obter(id);
        }

        public List<Models.Licença> ObterTodos()
        {
            DAL.LicençaDAL sbd = new DAL.LicençaDAL();
            return sbd.ObterTodos();
        }

        public List<Models.Licença> BuscarLicençaCli(int id)
        {
            DAL.LicençaDAL sbd = new DAL.LicençaDAL();
            return sbd.BuscarLicençaCli(id);
        }

        public List<Models.Licença> Pesquisar(string nome)
        {
            DAL.LicençaDAL lbd = new DAL.LicençaDAL();

            return lbd.Pesquisar(nome);
        }

        public bool Excluir(int id)
        {
            DAL.LicençaDAL lbd = new DAL.LicençaDAL();
            return lbd.Excluir(id);
        }

        public Licença BuscarLicença(int id)
        {
            return new Licença().BuscarLicença(id);
        }

        public Licença_Documento BuscarArquivo(int id)
        {
            DAL.LicençaDAL lbd = new DAL.LicençaDAL();
            return lbd.GetArquivo(id);
        }

        public bool Att(int id, string Versao, string dtVenc)
        {
            bool operacao = false;
            DAL.LicençaDAL lbd = new DAL.LicençaDAL();

            operacao = lbd.Att(id, Versao, dtVenc);

            return operacao;
        }
    }
}
