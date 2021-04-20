using HelloWorld.DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace Estagio.DAL
{
    public class EstadoDAL
    {
        MySQLPersistencia bd = new MySQLPersistencia();
        public List<Models.Estado> ObterTodos()
        {

            List<Models.Estado> dados = new List<Models.Estado>();

            try
            {
                string sql = @"select * from estado";
                DataTable dt = bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Estado estado = new Models.Estado();
                    estado.Id = Convert.ToInt32(row["id"]);
                    estado.Nome = row["nome"].ToString();
                    estado.Uf = row["uf"].ToString();
                    dados.Add(estado);
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
        public Models.Estado GetEstado(string nome)
        {
            Models.Estado estado = new Models.Estado();
            try
            {
                string sql = @"select * from estado where uf like @nome";
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("@nome", nome);
                DataTable dt = bd.ExecutarSelect(sql, parametros);
                foreach (DataRow row in dt.Rows)
                {
                    estado.Id = (int)row["id"];
                    estado.Nome = row["nome"].ToString();
                    estado.Uf = row["uf"].ToString();
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
            return estado;
        }

        public Models.Estado GetEstado(int id)
        {
            Models.Estado estado = new Models.Estado();
            try
            {
                string sql = @"select * from estado where id =" + id;

                DataTable dt = bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    estado.Id = (int)row["id"];
                    estado.Nome = row["nome"].ToString();
                    estado.Uf = row["uf"].ToString();
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
            return estado;
        }
    }
}
