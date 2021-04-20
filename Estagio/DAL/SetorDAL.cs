using HelloWorld.DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace Estagio.DAL
{
    public class SetorDAL
    {
        MySQLPersistencia _bd = new MySQLPersistencia();//chamando banco de dados estabelecido na classe MYSQLPersistencia

        //Cadastrar um Novo Setor
        public bool Criar(Models.Setor setor)
        {
            //mapeamento Objeto-Relacional (ORM);
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into setor (Descriçao)
                              values(@descriçao)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@descriçao", setor.Descriçao);

                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
                if (linhasAfetadas > 0)
                {
                    setor.Id = _bd.UltimoId;
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

            string select = @"select count(*) as conta from setor where 
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
        public Models.Setor Obter(int id)
        {
            Models.Setor setor = null;

            string select = @"select * 
                              from setor 
                              where id = " + id;

            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                setor = Map(dt.Rows[0]);
            }

            return setor;

        }

        //obter linha de uma tabela do banco de acordo com um nome passado, e jogando para um objeto
        public List<Models.Setor> Pesquisar(string descriçao)
        {

            List<Models.Setor> setores = new List<Models.Setor>();

            string select = @"select * 
                              from setor 
                              where descriçao like @descriçao";

            var parametros = _bd.GerarParametros();
            parametros.Add("@descriçao", "%" + descriçao + "%");

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                setores.Add(Map(row));
            }

            return setores;

        }

        //faz o mapeamento, jogando oq tem na linha do banco em um objeto
        internal Models.Setor Map(DataRow row)
        {
            Models.Setor setor = new Models.Setor();
            setor.Id = Convert.ToInt32(row["id"]);
            setor.Descriçao = row["descriçao"].ToString();

            return setor;
        }

        //Excluir uma linha da tabela passando o seu id
        public bool Excluir(int id)
        {
            string select = @"delete 
                              from setor 
                              where id = " + id;

            return _bd.ExecutarNonQuery(select) > 0;

        }

        public List<Models.Setor> ObterTodos()
        {

            List<Models.Setor> dados = new List<Models.Setor>();

            try
            {
                string sql = @"select * from setor";
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Setor setor = new Models.Setor();
                    setor.Id = Convert.ToInt32(row["id"]);
                    setor.Descriçao = row["descriçao"].ToString();
                    dados.Add(setor);
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


        //Editar uma linha da tabela passando um objeto
        public bool Editar(Models.Setor setor)
        {
            int linhasAfetadas = 0;
            try
            {
                string update = @"update setor set Descriçao = @descriçao where Id =" + setor.Id;

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@descriçao", setor.Descriçao);

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
