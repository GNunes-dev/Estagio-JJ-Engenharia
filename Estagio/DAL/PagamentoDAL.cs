using HelloWorld.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Estagio.DAL
{
    public class PagamentoDAL
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
                string insert = @"insert into pagamento (Descriçao, DtVencimento, Valor, FormaPag, Quitado, DtPagamento, ValorParcial)
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

        public List<Models.Pagamento> ObterTodos()
        {

            List<Models.Pagamento> dados = new List<Models.Pagamento>();

            try
            {
                string sql = @"select * from pagamento";
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Pagamento pagamento = new Models.Pagamento();
                    pagamento.Id = Convert.ToInt32(row["Id"]);
                    pagamento.Descriçao = row["Descriçao"].ToString();
                    pagamento.FormaPag = row["FormaPag"].ToString();
                    pagamento.dtVencimento = row["DtVencimento"].ToString();
                    pagamento.Valor = Convert.ToDouble(row["Valor"]);
                    pagamento.Quitado = Convert.ToBoolean(row["Quitado"]);
                    pagamento.dtPagamento = row["DtPagamento"].ToString();
                    pagamento.ValorParcial = Convert.ToDouble(row["ValorParcial"]);
                    dados.Add(pagamento);
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
                string update = @"update pagamento set Quitado = @Quitado, DtPagamento = @DtPagamento, ValorParcial = @ValorParcial where Id =" + id;

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
            Models.Pagamento pagamento = new Models.Pagamento();

            pagamento = Obter(id);
            int linhasAfetadas = 0;
            bool quitado = false;
            double valorparcial = 0;
            string dt = "";
            int nid = 0;

            if (pagamento.Valor == pagamento.ValorParcial)
                nid = id;
            else
            {
                List<Models.Pagamento> receb = ObterExtorno(id, pagamento.Valor, pagamento.Descriçao);

                foreach (var d in receb)
                {
                    if (d.ValorParcial == pagamento.Valor || d.Quitado == false)
                    {
                        nid = d.Id;
                        valorparcial = d.ValorParcial - pagamento.ValorParcial;
                    }
                }
                Excluir(id);
            }
            try
            {
                string update = @"update pagamento set Quitado = @Quitado, DtPagamento = @DtPagamento, ValorParcial = @ValorParcial where Id =" + nid;

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
                              from pagamento 
                              where id = " + id;

            return _bd.ExecutarNonQuery(select) > 0;

        }

        public List<Models.Pagamento> ObterExtorno(int id, double valor, string desc)
        {

            List<Models.Pagamento> dados = new List<Models.Pagamento>();

            try
            {
                string sql = @"select * from pagamento where Descriçao like @Descriçao";

                var parametros = _bd.GerarParametros();
                parametros.Add("@Descriçao", "%" + desc + "%");

                DataTable dt = _bd.ExecutarSelect(sql, parametros);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Pagamento pagamento = new Models.Pagamento();
                    pagamento.Id = Convert.ToInt32(row["Id"]);
                    pagamento.Descriçao = row["Descriçao"].ToString();
                    pagamento.FormaPag = row["FormaPag"].ToString();
                    pagamento.dtVencimento = row["DtVencimento"].ToString();
                    pagamento.Valor = Convert.ToDouble(row["Valor"]);
                    pagamento.Quitado = Convert.ToBoolean(row["Quitado"]);
                    pagamento.dtPagamento = row["DtPagamento"].ToString();
                    pagamento.ValorParcial = Convert.ToDouble(row["ValorParcial"]);
                    dados.Add(pagamento);
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
                string insert = @"insert into pagamento (Descriçao, DtVencimento, Valor, FormaPag, Quitado, DtPagamento, ValorParcial)
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

            Models.Pagamento pagamento = new Models.Pagamento();
            pagamento = Obter(id);
            pagamento.ValorParcial = pagamento.ValorParcial + valorparcial;

            try
            {
                if (pagamento.ValorParcial > pagamento.Valor)
                {
                    return false;
                }
                else
                {
                    if (pagamento.Valor == pagamento.ValorParcial)
                    {

                        GravarParcial(pagamento.Valor, pagamento.dtVencimento, pagamento.Descriçao, pagamento.FormaPag, valorparcial, dt[0]);

                        string update = @"update pagamento set ValorParcial = @ValorParcial, Quitado = @Quitado where Id =" + id;

                        //var parametros = _bd.GerarParametros();
                        Dictionary<string, object> parametros = new Dictionary<string, object>();
                        parametros.Add("@ValorParcial", pagamento.ValorParcial);
                        parametros.Add("@Quitado", true);

                        linhasAfetadas = _bd.ExecutarNonQuery(update, parametros);
                    }
                    else
                    {

                        GravarParcial(pagamento.Valor, pagamento.dtVencimento, pagamento.Descriçao, pagamento.FormaPag, valorparcial, dt[0]);
                        string update = @"update pagamento set ValorParcial = @ValorParcial where Id =" + id;

                        //var parametros = _bd.GerarParametros();
                        Dictionary<string, object> parametros = new Dictionary<string, object>();
                        parametros.Add("@ValorParcial", pagamento.ValorParcial);

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

        public Models.Pagamento Obter(int id)
        {
            Models.Pagamento pagamento = null;

            string select = @"select * 
                              from pagamento 
                              where id = " + id;

            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                pagamento = Map(dt.Rows[0]);
            }

            return pagamento;

        }

        internal Models.Pagamento Map(DataRow row)
        {

            Models.Pagamento pagamento = new Models.Pagamento();

            pagamento.Id = Convert.ToInt32(row["Id"]);
            pagamento.FormaPag = row["FormaPag"].ToString();
            pagamento.Descriçao = row["Descriçao"].ToString();
            pagamento.dtVencimento = row["DtVencimento"].ToString();
            pagamento.dtPagamento = row["DtPagamento"].ToString();
            pagamento.Valor = Convert.ToDouble(row["Valor"]);
            pagamento.ValorParcial = Convert.ToDouble(row["ValorParcial"]);
            pagamento.Quitado = Convert.ToBoolean(row["Quitado"]);

            return pagamento;
        }
    }
}
