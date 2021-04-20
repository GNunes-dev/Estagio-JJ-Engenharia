using HelloWorld.DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace Estagio.DAL
{
    public class ParametroDAL
    {
        MySQLPersistencia _bd = new MySQLPersistencia();//chamando banco de dados estabelecido na classe MYSQLPersistencia

        //Cadastrar um Novo Parametro
        public bool Criar(Models.Parametro parametro)
        {
            //mapeamento Objeto-Relacional (ORM);
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into parametro (Razao, NomeFantasia, CNPJ, IE, DataInicio, Endereco, Bairro, CEP, Email, Telefone, Id_c, Id_e)
                              values(@razao,  @nomefantasia, @cnpj, @ie, @datainicio, @endereco, @bairro, @cep, @email, @telefone, @Id_c, @Id_e)";

                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("@razao", parametro.Razao);
                parametros.Add("@nomefantasia", parametro.NomeFantasia);
                parametros.Add("@cnpj", parametro.Cnpj);
                parametros.Add("@ie", parametro.Ie);
                parametros.Add("@datainicio", parametro.DataInicio);
                parametros.Add("@endereco", parametro.Endereco);
                parametros.Add("@bairro", parametro.Bairro);
                parametros.Add("@cep", parametro.Cep);
                parametros.Add("@email", parametro.Email);
                parametros.Add("@telefone", parametro.Telefone);
                parametros.Add("@Id_c", parametro.CidadeId);
                parametros.Add("@Id_e", parametro.EstadoId);


                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
                if (linhasAfetadas > 0)
                {
                    parametro.Id = _bd.UltimoId;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }


        public bool obterParametro()
        {
            Models.Parametro parametro = null;

            string select = @"select * 
                              from parametro";


            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                return true;
            }

            return false;

        }
    }
}
