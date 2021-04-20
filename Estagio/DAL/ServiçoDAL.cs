using Estagio.Models;
using HelloWorld.DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace Estagio.DAL
{
    public class ServiçoDAL
    {
        MySQLPersistencia _bd = new MySQLPersistencia();//chamando banco de dados estabelecido na classe MYSQLPersistencia

        //Cadastrar um novo Serviço
        public bool Criar(Models.Serviço serviço)
        {
            //mapeamento Objeto-Relacional (ORM);
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into serviço (Nome, Descriçao, Valor, Id_Set)
                              values(@nome,  @descriçao, @valor, @id_set)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@nome", serviço.Nome);
                parametros.Add("@descriçao", serviço.Descriçao);
                parametros.Add("@valor", serviço.Valor);
                parametros.Add("@id_set", serviço.IdSetor.Id);

                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
                if (linhasAfetadas > 0)
                {
                    serviço.Id = _bd.UltimoId;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }
        //Validar se ao cadastrar não existe outro login com o mesmo nome
        public bool validarnomeUnico(string nome)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@nome", nome);

            string select = @"select count(*) as conta from serviço where 
                                          nome = @nome";

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            int conta = 0;
            conta = Convert.ToInt32(dt.Rows[0]["conta"]);

            if (conta == 0)
                return false;
            else
                return true;
        }
        //obter linha de uma tabela do banco de acordo com um id passado, e jogando para um objeto
        public Models.Serviço Obter(int id)
        {
            Models.Serviço serviço = null;

            string select = @"select * 
                              from serviço 
                              where id = " + id;

            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                serviço = Map(dt.Rows[0]);
            }

            return serviço;

        }

        public List<Models.Serviço> BuscarServiçoSetor(int id_set)
        {

            List<Models.Serviço> dados = new List<Models.Serviço>();

            try
            {
                string sql = @"select * from serviço where id_set = " + id_set;

                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Serviço serviço = new Models.Serviço();
                    serviço.IdSetor = new Setor();

                    serviço.Id = Convert.ToInt32(row["id"]);
                    serviço.Descriçao = row["descriçao"].ToString();
                    serviço.Nome = row["nome"].ToString();
                    serviço.Valor = Convert.ToDouble(row["valor"]);

                    serviço.IdSetor.Id = Convert.ToInt32(row["id_set"]);
                    SetorDAL dal = new SetorDAL();
                    serviço.IdSetor = dal.Obter(serviço.IdSetor.Id);
                    dados.Add(serviço);
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
        public List<Models.Serviço> Pesquisar(string nome)
        {

            List<Models.Serviço> serviços = new List<Models.Serviço>();

            string select = @"select * 
                              from serviço 
                              where nome like @nome";

            var parametros = _bd.GerarParametros();
            parametros.Add("@nome", "%" + nome + "%");

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                serviços.Add(Map(row));
            }

            return serviços;

        }

        //faz o mapeamento, jogando oq tem na linha do banco em um objeto
        internal Models.Serviço Map(DataRow row)
        {
            Models.Serviço serviço = new Models.Serviço();
            serviço.IdSetor = new Setor();
            serviço.Id = Convert.ToInt32(row["id"]);
            serviço.Nome = row["nome"].ToString();
            serviço.Descriçao = row["descriçao"].ToString();
            serviço.Valor = Convert.ToDouble(row["valor"]);
            serviço.IdSetor.Id = Convert.ToInt32(row["id_set"]);
            SetorDAL dal = new SetorDAL();
            serviço.IdSetor = dal.Obter(serviço.IdSetor.Id);
            return serviço;
        }

        //Excluir uma linha da tabela passando o seu id
        public bool Excluir(int id)
        {
            string select = @"delete 
                              from serviço 
                              where id = " + id;

            return _bd.ExecutarNonQuery(select) > 0;

        }

        public List<Models.Serviço> ObterTodos()
        {

            List<Models.Serviço> dados = new List<Models.Serviço>();

            try
            {
                string sql = @"select * from serviço";
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Serviço serviço = new Models.Serviço();
                    serviço.IdSetor = new Setor();

                    serviço.Id = Convert.ToInt32(row["id"]);
                    serviço.Descriçao = row["descriçao"].ToString();
                    serviço.Nome = row["nome"].ToString();
                    serviço.Valor = Convert.ToDouble(row["valor"]);
                    serviço.IdSetor.Id = Convert.ToInt32(row["id_set"]);
                    SetorDAL dal = new SetorDAL();
                    serviço.IdSetor = dal.Obter(serviço.IdSetor.Id);
                    dados.Add(serviço);
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
        public bool Editar(Models.Serviço serviço)
        {
            int linhasAfetadas = 0;
            try
            {
                string update = @"update serviço set Nome = @nome, Descriçao = @descriçao, Valor = @valor, Id_Set = @id_set where Id =" + serviço.Id;

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@nome", serviço.Nome);
                parametros.Add("@descriçao", serviço.Descriçao);
                parametros.Add("@valor", serviço.Valor);
                parametros.Add("@id_set", serviço.IdSetor.Id);

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
