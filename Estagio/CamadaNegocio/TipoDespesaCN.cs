using Estagio.Models;
using System.Collections.Generic;

namespace Estagio.CamadaNegocio
{
    public class TipoDespesaCN
    {
        public (bool, string) Criar(Models.TipoDespesa tipoDespesa)
        {
            string msg = "";
            bool operacao = false;
            DAL.TipoDespesaDAL tbd = new DAL.TipoDespesaDAL();

            if (tbd.validarnomeUnico(tipoDespesa.Descriçao))
            {
                msg = "Tipo de Despesa já cadastrado.";
            }
            else
            {
                operacao = tbd.Criar(tipoDespesa);
            }

            return (operacao, msg);
        }

        public Models.TipoDespesa Obter(int id)
        {
            DAL.TipoDespesaDAL tbd = new DAL.TipoDespesaDAL();
            return tbd.Obter(id);
        }

        public List<Models.TipoDespesa> ObterTodos()
        {
            DAL.TipoDespesaDAL sbd = new DAL.TipoDespesaDAL();
            return sbd.ObterTodos();
        }
        public List<Models.TipoDespesa> Pesquisar(string descriçao)
        {
            DAL.TipoDespesaDAL tbd = new DAL.TipoDespesaDAL();

            return tbd.Pesquisar(descriçao);
        }

        public bool Excluir(int id)
        {
            DAL.TipoDespesaDAL tbd = new DAL.TipoDespesaDAL();
            return tbd.Excluir(id);
        }

        public TipoDespesa BuscarTipoDespesa(int id)
        {
            return new TipoDespesa().BuscarTipoDespesa(id);
        }

        public bool Editar(Models.TipoDespesa tipoDespesa)
        {
            bool operacao = false;
            DAL.TipoDespesaDAL tbd = new DAL.TipoDespesaDAL();

            operacao = tbd.Editar(tipoDespesa);

            return operacao;
        }
    }
}
