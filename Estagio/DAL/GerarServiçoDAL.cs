using HelloWorld.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Estagio.DAL
{
    public class GerarServiçoDAL
    {
        MySQLPersistencia _bd = new MySQLPersistencia();//chamando banco de dados estabelecido na classe MYSQLPersistencia

        //Cadastrar uma nova licença pegando informações de cliente e orgao de licenciamento
        public int Criar(Models.GerarServiço gerarserv)
        {
            //mapeamento Objeto-Relacional (ORM);
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into gerarserviço (Id_Client, Id_Func, Id_Seto, Descriçao, DtInicial, DtPrevFinal, DtFinal, Endereço, Bairro, Cep, Id_Estad, Id_Cidad, ValorTotal, Status)
                              values(@Id_Client, @Id_Func, @Id_Seto, @Descriçao, @DtInicial, @DtPrevFinal, @DtFinal, @Endereço, @Bairro, @Cep, @Id_Estad, @Id_Cidad, @ValorTotal, @Status)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Id_Client", gerarserv.clienteId.Id);
                parametros.Add("@Id_Func", gerarserv.funcionarioiId);
                parametros.Add("@Id_Seto", gerarserv.setorId);
                parametros.Add("@Descriçao", gerarserv.Descriçao);
                parametros.Add("@DtInicial", gerarserv.dtInicio);
                parametros.Add("@DtPrevFinal", gerarserv.dtPrevFim);
                parametros.Add("@DtFinal", gerarserv.dtFim);
                parametros.Add("@Endereço", gerarserv.Endereco);
                parametros.Add("@Bairro", gerarserv.Bairro);
                parametros.Add("@Cep", gerarserv.Cep);
                parametros.Add("@Id_Estad", gerarserv.estadoId);
                parametros.Add("@Id_Cidad", gerarserv.cidadeId);
                parametros.Add("@ValorTotal", gerarserv.ValorTotal);
                parametros.Add("@Status", gerarserv.Status);

                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
                if (linhasAfetadas > 0)
                {
                    gerarserv.Id = _bd.UltimoId;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            if (linhasAfetadas > 0)
                return gerarserv.Id;
            else
                return -1;

        }

        //grava a lista de serviço em orçamento
        public bool CriarItensServiço(int id_servico, int id_gerarserv)
        {
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into itens_serviço (Id_gerarserv, Id_servico)
                              values(@Id_gerarserv, @Id_servico)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Id_gerarserv", id_gerarserv);
                parametros.Add("@Id_servico", id_servico);

                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }

        public List<Models.Serviço> BuscarItensServiço(int id)
        {
            List<Models.ItensServ> dados = new List<Models.ItensServ>();
            List<Models.Serviço> serv = new List<Models.Serviço>();

            DAL.ServiçoDAL sbd = new DAL.ServiçoDAL();
            try
            {
                string sql = @"select * from itens_serviço
                              where Id_gerarserv =" + id;

                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.ItensServ itens = new Models.ItensServ();

                    itens.Idgerarserv = Convert.ToInt32(row["Id_gerarserv"]);
                    itens.Idserv = Convert.ToInt32(row["Id_servico"]);

                    serv.Add(sbd.Obter(itens.Idserv));
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
            return serv;
        }

        public List<Models.GerarServiço> ObterTodos()
        {

            List<Models.GerarServiço> dados = new List<Models.GerarServiço>();

            try
            {
                string sql = @"select * from gerarserviço";
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.GerarServiço serviço = new Models.GerarServiço();
                    serviço.clienteId = new Models.Cliente();

                    serviço.Id = Convert.ToInt32(row["Id"]);
                    serviço.clienteId.Id = Convert.ToInt32(row["Id_Client"]);
                    serviço.funcionarioiId = Convert.ToInt32(row["Id_Func"]);
                    serviço.setorId = Convert.ToInt32(row["Id_Seto"]);
                    serviço.Descriçao = row["Descriçao"].ToString();
                    serviço.dtInicio = row["DtInicial"].ToString();
                    serviço.dtPrevFim = row["DtPrevFinal"].ToString();
                    serviço.dtFim = row["DtFinal"].ToString();
                    serviço.Endereco = row["Endereço"].ToString();
                    serviço.Bairro = row["Bairro"].ToString();
                    serviço.Cep = row["Cep"].ToString();
                    serviço.estadoId = Convert.ToInt32(row["Id_Estad"]);
                    serviço.cidadeId = Convert.ToInt32(row["Id_Cidad"]);
                    serviço.ValorTotal = Convert.ToDouble(row["ValorTotal"]);
                    serviço.Status = row["Status"].ToString();

                    ClienteDAL dal = new ClienteDAL();
                    serviço.clienteId = dal.Obter(serviço.clienteId.Id);

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

        public List<Models.GerarServiço> BuscarServiçoCli(int id)
        {

            List<Models.GerarServiço> dados = new List<Models.GerarServiço>();

            try
            {
                string sql = @"select * from gerarserviço where Id_Client =" + id;
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.GerarServiço serviço = new Models.GerarServiço();
                    serviço.clienteId = new Models.Cliente();

                    serviço.Id = Convert.ToInt32(row["Id"]);
                    serviço.clienteId.Id = Convert.ToInt32(row["Id_Client"]);
                    serviço.funcionarioiId = Convert.ToInt32(row["Id_Func"]);
                    serviço.setorId = Convert.ToInt32(row["Id_Seto"]);
                    serviço.Descriçao = row["Descriçao"].ToString();
                    serviço.dtInicio = row["DtInicial"].ToString();
                    serviço.dtPrevFim = row["DtPrevFinal"].ToString();
                    serviço.dtFim = row["DtFinal"].ToString();
                    serviço.Endereco = row["Endereço"].ToString();
                    serviço.Bairro = row["Bairro"].ToString();
                    serviço.Cep = row["Cep"].ToString();
                    serviço.estadoId = Convert.ToInt32(row["Id_Estad"]);
                    serviço.cidadeId = Convert.ToInt32(row["Id_Cidad"]);
                    serviço.ValorTotal = Convert.ToDouble(row["ValorTotal"]);
                    serviço.Status = row["Status"].ToString();

                    ClienteDAL dal = new ClienteDAL();
                    serviço.clienteId = dal.Obter(serviço.clienteId.Id);

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

        public bool Att(int id, string status)
        {
            int linhasAfetadas = 0;
            try
            {
                string update = @"update gerarserviço set Status = @Status where Id =" + id;

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Status", status);

                linhasAfetadas = _bd.ExecutarNonQuery(update, parametros);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }

        //obter linha de uma tabela do banco de acordo com um id passado, e jogando para um objeto
        public Models.GerarServiço Obter(int id)
        {
            Models.GerarServiço gerarserv = null;

            string select = @"select * 
                              from gerarserviço 
                              where id = " + id;

            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                gerarserv = Map(dt.Rows[0]);
            }

            return gerarserv;

        }

        //faz o mapeamento, jogando oq tem na linha do banco em um objeto
        internal Models.GerarServiço Map(DataRow row)
        {
           
            Models.GerarServiço serviço = new Models.GerarServiço();
            serviço.clienteId = new Models.Cliente();

            serviço.Id = Convert.ToInt32(row["Id"]);
            serviço.clienteId.Id = Convert.ToInt32(row["Id_Client"]);
            serviço.funcionarioiId = Convert.ToInt32(row["Id_Func"]);
            serviço.setorId = Convert.ToInt32(row["Id_Seto"]);
            serviço.Descriçao = row["Descriçao"].ToString();
            serviço.dtInicio = row["DtInicial"].ToString();
            serviço.dtPrevFim = row["DtPrevFinal"].ToString();
            serviço.dtFim = row["DtFinal"].ToString();
            serviço.Endereco = row["Endereço"].ToString();
            serviço.Bairro = row["Bairro"].ToString();
            serviço.Cep = row["Cep"].ToString();
            serviço.estadoId = Convert.ToInt32(row["Id_Estad"]);
            serviço.cidadeId = Convert.ToInt32(row["Id_Cidad"]);
            serviço.ValorTotal = Convert.ToDouble(row["ValorTotal"]);
            serviço.Status = row["Status"].ToString();

            ClienteDAL dal = new ClienteDAL();
            serviço.clienteId = dal.Obter(serviço.clienteId.Id);

            return serviço;
        }


        //Excluir uma linha da tabela passando o seu id
        public bool Excluir(int id)
        {
            string select = @"delete 
                              from gerarserviço 
                              where id = " + id;

            return _bd.ExecutarNonQuery(select) > 0;

        }

    }
}
