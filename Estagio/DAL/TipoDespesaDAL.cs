using HelloWorld.DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace Estagio.DAL
{
    public class TipoDespesaDAL
    {

        MySQLPersistencia _bd = new MySQLPersistencia();//chamando banco de dados estabelecido na classe MYSQLPersistencia

        //Cadastrar um Novo tipo de despesa
        public bool Criar(Models.TipoDespesa tipoDespesa)
        {
            //mapeamento Objeto-Relacional (ORM);
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into tipodespesa (Descriçao)
                              values(@descriçao)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@descriçao", tipoDespesa.Descriçao);

                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
                if (linhasAfetadas > 0)
                {
                    tipoDespesa.Id = _bd.UltimoId;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }

        //Validar se ao cadastrar não existe outro login com o mesmo nome
        public bool validarnomeUnico(string descriçao)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@descriçao", descriçao);

            string select = @"select count(*) as conta from tipodespesa where 
                                          descriçao = @descriçao";

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            int conta = 0;
            conta = Convert.ToInt32(dt.Rows[0]["conta"]);

            if (conta == 0)
                return false;
            else
                return true;
        }

        //obter linha de uma tabela do banco de acordo com um id passado, e jogando para um objeto
        public Models.TipoDespesa Obter(int id)
        {
            Models.TipoDespesa tipoDespesa = null;

            string select = @"select * 
                              from tipodespesa 
                              where id = " + id;

            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                tipoDespesa = Map(dt.Rows[0]);
            }

            return tipoDespesa;

        }

        public List<Models.TipoDespesa> ObterTodos()
        {

            List<Models.TipoDespesa> dados = new List<Models.TipoDespesa>();

            try
            {
                string sql = @"select * from tipodespesa";
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.TipoDespesa despesa = new Models.TipoDespesa();
                    despesa.Id = Convert.ToInt32(row["id"]);
                    despesa.Descriçao = row["descriçao"].ToString();
                    dados.Add(despesa);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _bd.Fechar();
            }
            return dados;
        }

        //obter linha de uma tabela do banco de acordo com um nome passado, e jogando para um objeto
        public List<Models.TipoDespesa> Pesquisar(string descriçao)
        {

            List<Models.TipoDespesa> tipoDespesas = new List<Models.TipoDespesa>();

            string select = @"select * 
                              from tipodespesa 
                              where descriçao like @descriçao";

            var parametros = _bd.GerarParametros();
            parametros.Add("@descriçao", "%" + descriçao + "%");

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                tipoDespesas.Add(Map(row));
            }

            return tipoDespesas;

        }

        //faz o mapeamento, jogando oq tem na linha do banco em um objeto
        internal Models.TipoDespesa Map(DataRow row)
        {
            Models.TipoDespesa tipoDespesa = new Models.TipoDespesa();
            tipoDespesa.Id = Convert.ToInt32(row["id"]);
            tipoDespesa.Descriçao = row["descriçao"].ToString();

            return tipoDespesa;
        }

        //Excluir uma linha da tabela passando o seu id
        public bool Excluir(int id)
        {
            string select = @"delete 
                              from tipodespesa 
                              where id = " + id;

            return _bd.ExecutarNonQuery(select) > 0;

        }

        //Editar uma linha da tabela passando um objeto
        public bool Editar(Models.TipoDespesa tipoDespesa)
        {
            int linhasAfetadas = 0;
            try
            {
                string update = @"update tipodespesa set Descriçao = @descriçao where Id =" + tipoDespesa.Id;

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@descriçao", tipoDespesa.Descriçao);

                linhasAfetadas = _bd.ExecutarNonQuery(update, parametros);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }
    }
}
