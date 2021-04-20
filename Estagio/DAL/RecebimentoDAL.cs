using HelloWorld.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Estagio.DAL
{
    public class RecebimentoDAL
    {
        MySQLPersistencia _bd = new MySQLPersistencia();//chamando banco de dados estabelecido na classe MYSQLPersistencia
        public bool GravarPagamento(double valor, string data, string desc, string formapag)
        {
            int linhasAfetadas = 0;
            bool quitado = false;
            double parcial = 0;
            string dtpagamento = "";
            try
            {
                string insert = @"insert into recebimento (Descriçao, DtVencimento, Valor, FormaPag, Quitado, DtPagamento, ValorParcial)
                              values(@Descriçao, @DtVencimento, @Valor, @FormaPag, @Quitado, @DtPagamento, @ValorParcial)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Descriçao", desc);
                parametros.Add("@DtVencimento", data);
                parametros.Add("@Valor", valor);
                parametros.Add("@FormaPag", formapag);
                parametros.Add("@Quitado", quitado);
                parametros.Add("@DtPagamento", dtpagamento);
                parametros.Add("@ValorParcial", parcial);
                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);

            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }

        public List<Models.Recebimento> ObterTodos()
        {

            List<Models.Recebimento> dados = new List<Models.Recebimento>();

            try
            {
                string sql = @"select * from recebimento";
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Recebimento recebimento = new Models.Recebimento();
                    recebimento.Id = Convert.ToInt32(row["Id"]);
                    recebimento.Descriçao = row["Descriçao"].ToString();
                    recebimento.FormaPag = row["FormaPag"].ToString();
                    recebimento.dtVencimento = row["DtVencimento"].ToString();
                    recebimento.Valor = Convert.ToDouble(row["Valor"]);
                    recebimento.Quitado = Convert.ToBoolean(row["Quitado"]);
                    recebimento.dtPagamento = row["DtPagamento"].ToString();
                    recebimento.ValorParcial = Convert.ToDouble(row["ValorParcial"]);
                    dados.Add(recebimento);
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

        public bool Quitar(int id, double valor)
        {
            int linhasAfetadas = 0;
            bool quitado = true;
            DateTime thisDay = DateTime.Today;

            string dtpagamento = thisDay.ToString();
            var dt = dtpagamento.Split(' ');
            double valorparcial = valor;
            try
            {
                string update = @"update recebimento set Quitado = @Quitado, DtPagamento = @DtPagamento, ValorParcial = @ValorParcial where Id =" + id;

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Quitado", quitado);
                parametros.Add("@ValorParcial", valorparcial);
                parametros.Add("@DtPagamento", dt[0]);

                linhasAfetadas = _bd.ExecutarNonQuery(update, parametros);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }

        public bool Extornar(int id) //revisar
        {
            Models.Recebimento recebimento = new Models.Recebimento();

            recebimento = Obter(id);
            int linhasAfetadas = 0;
            bool quitado = false;
            double valorparcial = 0;
            string dt = "";
            int nid = 0;

            if (recebimento.Valor == recebimento.ValorParcial)
                nid = id;
            else
            {
                List<Models.Recebimento> receb = ObterExtorno(id, recebimento.Valor, recebimento.Descriçao);

                foreach (var d in receb)
                {
                    if (d.ValorParcial == recebimento.Valor || d.Quitado == false)
                    {
                        nid = d.Id;
                        valorparcial = d.ValorParcial - recebimento.ValorParcial;
                    }
                }
                Excluir(id);
            }        
            try
            {
                string update = @"update recebimento set Quitado = @Quitado, DtPagamento = @DtPagamento, ValorParcial = @ValorParcial where Id =" + nid;

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Quitado", quitado);
                parametros.Add("@ValorParcial", valorparcial);
                parametros.Add("@DtPagamento", dt);

                linhasAfetadas = _bd.ExecutarNonQuery(update, parametros);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }
        public bool Excluir(int id)
        {
            string select = @"delete 
                              from recebimento 
                              where id = " + id;

            return _bd.ExecutarNonQuery(select) > 0;

        }

        public List<Models.Recebimento> ObterExtorno(int id, double valor, string desc)
        {

            List<Models.Recebimento> dados = new List<Models.Recebimento>();

            try
            {
                string sql = @"select * from recebimento where Descriçao like @Descriçao";

                var parametros = _bd.GerarParametros();
                parametros.Add("@Descriçao", "%" + desc + "%");

                DataTable dt = _bd.ExecutarSelect(sql, parametros);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Recebimento recebimento = new Models.Recebimento();
                    recebimento.Id = Convert.ToInt32(row["Id"]);
                    recebimento.Descriçao = row["Descriçao"].ToString();
                    recebimento.FormaPag = row["FormaPag"].ToString();
                    recebimento.dtVencimento = row["DtVencimento"].ToString();
                    recebimento.Valor = Convert.ToDouble(row["Valor"]);
                    recebimento.Quitado = Convert.ToBoolean(row["Quitado"]);
                    recebimento.dtPagamento = row["DtPagamento"].ToString();
                    recebimento.ValorParcial = Convert.ToDouble(row["ValorParcial"]);
                    dados.Add(recebimento);
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


        public bool GravarParcial(double valor, string data, string desc, string formapag, double valorparcial, string datapag)
        {
            int linhasAfetadas = 0;
            bool quitado = true;

            try
            {
                string insert = @"insert into recebimento (Descriçao, DtVencimento, Valor, FormaPag, Quitado, DtPagamento, ValorParcial)
                              values(@Descriçao, @DtVencimento, @Valor, @FormaPag, @Quitado, @DtPagamento, @ValorParcial)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Descriçao", desc);
                parametros.Add("@DtVencimento", data);
                parametros.Add("@Valor", valor);
                parametros.Add("@FormaPag", formapag);
                parametros.Add("@Quitado", quitado);
                parametros.Add("@DtPagamento", datapag);
                parametros.Add("@ValorParcial", valorparcial);
                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);

            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }

        public bool QuitarParcial(double valorparcial, int id)
        {
            int linhasAfetadas = 0;
            DateTime thisDay = DateTime.Today;

            string dtpagamento = thisDay.ToString();
            var dt = dtpagamento.Split(' ');

            Models.Recebimento recebimento = new Models.Recebimento();
            recebimento = Obter(id);
            recebimento.ValorParcial = recebimento.ValorParcial + valorparcial;
            
            try
            {
                if(recebimento.ValorParcial > recebimento.Valor)
                {
                    return false;
                }
                else
                {
                    if(recebimento.Valor == recebimento.ValorParcial)
                    {

                        GravarParcial(recebimento.Valor, recebimento.dtVencimento, recebimento.Descriçao, recebimento.FormaPag, valorparcial, dt[0]);

                        string update = @"update recebimento set ValorParcial = @ValorParcial, Quitado = @Quitado where Id =" + id;

                        //var parametros = _bd.GerarParametros();
                        Dictionary<string, object> parametros = new Dictionary<string, object>();
                        parametros.Add("@ValorParcial", recebimento.ValorParcial);
                        parametros.Add("@Quitado", true);

                        linhasAfetadas = _bd.ExecutarNonQuery(update, parametros);
                    }
                    else
                    {

                        GravarParcial(recebimento.Valor, recebimento.dtVencimento, recebimento.Descriçao, recebimento.FormaPag, valorparcial, dt[0]);
                        string update = @"update recebimento set ValorParcial = @ValorParcial where Id =" + id;

                        //var parametros = _bd.GerarParametros();
                        Dictionary<string, object> parametros = new Dictionary<string, object>();
                        parametros.Add("@ValorParcial", recebimento.ValorParcial);

                        linhasAfetadas = _bd.ExecutarNonQuery(update, parametros);
                    }
                }
                
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }

        public Models.Recebimento Obter(int id)
        {
            Models.Recebimento recebimento = null;

            string select = @"select * 
                              from recebimento 
                              where id = " + id;

            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                recebimento = Map(dt.Rows[0]);
            }

            return recebimento;

        }

        internal Models.Recebimento Map(DataRow row)
        {

            Models.Recebimento recebimento = new Models.Recebimento();

            recebimento.Id = Convert.ToInt32(row["Id"]);
            recebimento.FormaPag = row["FormaPag"].ToString();
            recebimento.Descriçao = row["Descriçao"].ToString();
            recebimento.dtVencimento = row["DtVencimento"].ToString();
            recebimento.dtPagamento = row["DtPagamento"].ToString();
            recebimento.Valor = Convert.ToDouble(row["Valor"]);
            recebimento.ValorParcial = Convert.ToDouble(row["ValorParcial"]);
            recebimento.Quitado = Convert.ToBoolean(row["Quitado"]);

            return recebimento;
        }
    }
}
