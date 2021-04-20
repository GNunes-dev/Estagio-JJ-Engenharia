using HelloWorld.DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace Estagio.DAL
{
    public class OrgaoLicenciamentoDAL
    {

        MySQLPersistencia _bd = new MySQLPersistencia();//chamando banco de dados estabelecido na classe MYSQLPersistencia

        //Cadastrar um Novo Orgão de Licenciamento
        public bool Criar(Models.OrgaoLicenciamento orgao)
        {
            //mapeamento Objeto-Relacional (ORM);
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into orgaolicenciamento (Descriçao, Valor)
                              values(@descriçao, @valor)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@descriçao", orgao.Descriçao);
                parametros.Add("@valor", orgao.Valor);

                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
                if (linhasAfetadas > 0)
                {
                    orgao.Id = _bd.UltimoId;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }
        //Validar se ao cadastrar não existe outro login com o mesmo nome
        public bool validarnomeUnico(string descricao)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@descriçao", descricao);

            string select = @"select count(*) as conta from orgaolicenciamento where 
                                          descriçao = @descriçao";

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            int conta = 0;
            conta = Convert.ToInt32(dt.Rows[0]["conta"]);

            if (conta == 0)
                return false;
            else
                return true;
        }

        //obter todas as linhas da tabela
        public List<Models.OrgaoLicenciamento> ObterTodos()
        {

            List<Models.OrgaoLicenciamento> dados = new List<Models.OrgaoLicenciamento>();

            try
            {
                string sql = @"select * from orgaolicenciamento";
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.OrgaoLicenciamento orgao = new Models.OrgaoLicenciamento();
                    orgao.Id = Convert.ToInt32(row["id"]);
                    orgao.Descriçao = row["descriçao"].ToString();
                    orgao.Valor = Convert.ToDouble(row["valor"]);
                    dados.Add(orgao);
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

        //obter linha de uma tabela do banco de acordo com um id passado, e jogando para um objeto
        public Models.OrgaoLicenciamento Obter(int id)
        {
            Models.OrgaoLicenciamento orgao = null;

            string select = @"select * 
                              from orgaolicenciamento 
                              where id = " + id;

            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                orgao = Map(dt.Rows[0]);
            }

            return orgao;

        }

        //obter linha de uma tabela do banco de acordo com um nome passado, e jogando para um objeto
        public List<Models.OrgaoLicenciamento> Pesquisar(string descricao)
        {

            List<Models.OrgaoLicenciamento> orgao = new List<Models.OrgaoLicenciamento>();

            string select = @"select * 
                              from orgaolicenciamento 
                              where descriçao like @descriçao";

            var parametros = _bd.GerarParametros();
            parametros.Add("@descriçao", "%" + descricao + "%");

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                orgao.Add(Map(row));
            }

            return orgao;

        }

        //faz o mapeamento, jogando oq tem na linha do banco em um objeto
        internal Models.OrgaoLicenciamento Map(DataRow row)
        {
            Models.OrgaoLicenciamento orgao = new Models.OrgaoLicenciamento();
            orgao.Id = Convert.ToInt32(row["id"]);
            orgao.Descriçao = row["descriçao"].ToString();
            orgao.Valor = Convert.ToDouble(row["valor"]);

            return orgao;
        }

        //Excluir uma linha da tabela passando o seu id
        public bool Excluir(int id)
        {
            string select = @"delete 
                              from orgaolicenciamento
                              where id = " + id;

            return _bd.ExecutarNonQuery(select) > 0;

        }

        //Editar uma linha da tabela passando um objeto
        public bool Editar(Models.OrgaoLicenciamento orgao)
        {
            int linhasAfetadas = 0;
            try
            {
                string update = @"update orgaolicenciamento set Descriçao = @descriçao, Valor = @valor where Id =" + orgao.Id;

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@descriçao", orgao.Descriçao);
                parametros.Add("@valor", orgao.Valor);

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
