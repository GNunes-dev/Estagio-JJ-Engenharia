using HelloWorld.DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace Estagio.DAL
{
    public class CidadeDAL
    {
        MySQLPersistencia bd = new MySQLPersistencia();
        public List<Models.Cidade> ObterCidades_Estado(int id)
        {

            List<Models.Cidade> dados = new List<Models.Cidade>();

            try
            {
                string sql = @"select * from cidade where estado = " + id;
                DataTable dt = bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Cidade cidade = new Models.Cidade();
                    cidade.Id = (int)row["id"];
                    cidade.Nome = row["nome"].ToString();
                    cidade.EstadoId = (int)row["estado"];
                    dados.Add(cidade);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                bd.Fechar();
            }

            return dados;
        }
        public Models.Cidade GetCidade(string nome)
        {
            Models.Cidade cidade = new Models.Cidade();
            try
            {
                string sql = @"select * from cidade where nome like @nome";
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("@nome", nome);
                DataTable dt = bd.ExecutarSelect(sql, parametros);
                foreach (DataRow row in dt.Rows)
                {
                    cidade.Id = (int)row["id"];
                    cidade.Nome = row["nome"].ToString();
                    cidade.EstadoId = (int)row["estado"];
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                bd.Fechar();
            }
            return cidade;
        }

        public Models.Cidade GetCidade(int id)
        {
            Models.Cidade cidade = new Models.Cidade();
            try
            {
                string sql = @"select * from cidade where id =" + id;

                DataTable dt = bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    cidade.Id = (int)row["id"];
                    cidade.Nome = row["nome"].ToString();
                    cidade.EstadoId = (int)row["estado"];
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                bd.Fechar();
            }
            return cidade;
        }
    }
}
