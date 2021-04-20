//Provider para o MYSQL
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data; //Importar o DataTable (table em memória)

//using Oracle.Data.OracleClient;

namespace HelloWorld.DAL
{
    /// <summary>
    /// Oferecer suporte para acesso ao MYSQL.
    /// </summary>
    public class MySQLPersistencia
    {
        MySqlConnection _con;
        MySqlCommand _cmd; //executa as instruções SQL

        int _ultimoId = 0;
        public int UltimoId { get => _ultimoId; set => _ultimoId = value; }

        public MySQLPersistencia()
        {
            string strCon = System.Environment.GetEnvironmentVariable("MYSQLSTRCON");
            _con = new MySqlConnection(strCon);
            _cmd = _con.CreateCommand();
        }


        public void Abrir()
        {
            if (_con.State != System.Data.ConnectionState.Open)
                _con.Open();
        }

        public void Fechar()
        {
            _con.Close();
        }

        public Dictionary<string, object> GerarParametros()
        {
            return new Dictionary<string, object>();
        }

        /// <summary>
        /// Executa um comando SELECT
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        public DataTable ExecutarSelect(string select, Dictionary<string, object> parametros = null)
        {
            Abrir();
            _cmd.CommandText = select;
            DataTable dt = new DataTable();

            if (parametros != null)
            {
                foreach (var p in parametros)
                {
                    _cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            dt.Load(_cmd.ExecuteReader());

            Fechar();

            return dt;
        }


        /// <summary>
        /// Executa INSERT, DELETE, UPDATE E STORED PROCEDURE
        /// </summary>
        /// <param name="sql"></param>
        public int ExecutarNonQuery(string sql, Dictionary<string, object> parametros = null)
        {
            Abrir();
            _cmd.CommandText = sql;

            if (parametros != null)
            {
                _cmd.Parameters.Clear();
                foreach (var p in parametros)
                {
                    _cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            int linhasAfetadas = _cmd.ExecuteNonQuery();
            _ultimoId = (int)_cmd.LastInsertedId;

            Fechar();

            return linhasAfetadas;
        }

        public int Executar(string instrucao, Dictionary<string, object> parametros = null, Dictionary<string, byte[]> parametrosBinarios = null)
        {
            Abrir();
            _cmd.CommandText = instrucao;

            if (parametros != null)
            {
                _cmd.Parameters.Clear();
                foreach (var item in parametros)
                {
                    _cmd.Parameters.AddWithValue(item.Key, item.Value);

                    if (item.Key == "@imagem")
                        _cmd.Parameters[item.Key].MySqlDbType = MySqlDbType.Blob;
                }
            }
            if (parametrosBinarios != null)
            {
                foreach (var p in parametrosBinarios)
                {
                    _cmd.Parameters.Add(p.Key, MySqlDbType.Blob);
                    _cmd.Parameters[p.Key].Value = p.Value;
                }
            }
            //insert, delete, update...
            int linhasAfetadas = _cmd.ExecuteNonQuery();

            _ultimoId = (int)_cmd.LastInsertedId;

            Fechar();

            return linhasAfetadas;
        }

        public object ExecutarScalar(string sql, Dictionary<string, object> parametros = null, Dictionary<string, byte[]> parametrosBinarios = null)
        {
            Abrir();
            _cmd.CommandText = sql;

            if (parametros != null)
            {
                foreach (var p in parametros)
                {
                    _cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            if (parametrosBinarios != null)
            {
                foreach (var p in parametrosBinarios)
                {
                    _cmd.Parameters.Add(p.Key, MySqlDbType.Blob);
                    _cmd.Parameters[p.Key].Value = p.Value;
                }
            }


            object retorno = _cmd.ExecuteScalar();
            _ultimoId = (int)_cmd.LastInsertedId;

            Fechar();

            return retorno;
        }

    }
}
