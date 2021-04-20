using HelloWorld.DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace Estagio.DAL
{
    public class OrçamentoDAL
    {
        MySQLPersistencia _bd = new MySQLPersistencia();//chamando banco de dados estabelecido na classe MYSQLPersistencia

        //Cadastrar uma nova licença pegando informações de cliente e orgao de licenciamento
        public int Criar(Models.Orçamento orçamento)
        {
            //mapeamento Objeto-Relacional (ORM);
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into orçamento (Descriçao, DtVencimento, Id_Cliente, Id_Setor, FormaPag, ValorTotal)
                              values(@Descriçao, @DtVencimento, @Id_Cliente, @Id_Setor, @FormaPag, @ValorTotal)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Descriçao", orçamento.Descriçao);
                parametros.Add("@DtVencimento", orçamento.dtVencimento);
                parametros.Add("@Id_Cliente", orçamento.clienteId.Id);
                parametros.Add("@Id_Setor", orçamento.setorId.Id);
                parametros.Add("@FormaPag", orçamento.FormaPag);
                parametros.Add("@ValorTotal", orçamento.valorTotal);

                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
                if (linhasAfetadas > 0)
                {
                    orçamento.Id = _bd.UltimoId;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            if (linhasAfetadas > 0)
                return orçamento.Id;
            else
                return -1;
            
        }

        //grava a lista de serviço em orçamento
        public bool CriarOrçamentoServiço(int id, int idorça)
        {
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into orçamento_serviço (Id_Orçamento, Id_Serviço)
                              values(@Id_Orçamento, @Id_Serviço)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Id_Orçamento", idorça);
                parametros.Add("@Id_Serviço", id);

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
            List<Models.OrçamentoServ> dados = new List<Models.OrçamentoServ>();
            List<Models.Serviço> serv = new List<Models.Serviço>();

            DAL.ServiçoDAL sbd = new DAL.ServiçoDAL();
            try
            {
                string sql = @"select * from orçamento_serviço
                              where Id_Orçamento =" + id;

                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.OrçamentoServ itens = new Models.OrçamentoServ();

                    itens.Idor = Convert.ToInt32(row["Id_Orçamento"]);
                    itens.Idserv = Convert.ToInt32(row["Id_Serviço"]);

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


        public bool CriarOrçamentoLicença(int id, int idorça)
        {
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into orçamento_licença (Id_or, Id_li)
                              values(@Id_or, @Id_li)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Id_or", idorça);
                parametros.Add("@Id_li", id);

                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }

        public List<Models.Licença> BuscarItensLicença(int id)
        {
            List<Models.OrçamentoLic> dados = new List<Models.OrçamentoLic>();
            List<Models.Licença> lic = new List<Models.Licença>();

            DAL.LicençaDAL sbd = new DAL.LicençaDAL();
            try
            {
                string sql = @"select * from orçamento_licença
                              where Id_or =" + id;

                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.OrçamentoLic itens = new Models.OrçamentoLic();

                    itens.Id_or = Convert.ToInt32(row["Id_or"]);
                    itens.Id_lic = Convert.ToInt32(row["Id_li"]);

                    lic.Add(sbd.Obter(itens.Id_lic));
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
            return lic;
        }

        public bool ExcluiOrçaLic(int id, int idlic)
        {
            string select = @"delete 
                              from orçamento_licença 
                              where id_or = " + id + " and id_li = " + idlic;

            return _bd.ExecutarNonQuery(select) > 0;

        }

        public bool ExcluiOrçaServ(int id, int idserv)
        {
            string select = @"delete 
                              from orçamento_serviço 
                              where Id_Orçamento = " + id + " and Id_Serviço = " + idserv;

            return _bd.ExecutarNonQuery(select) > 0;

        }

        //Validar se ao cadastrar não existe outro login com o mesmo nome
        public bool validarnomeUnico(string descriçao)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@Nome", descriçao);

            string select = @"select count(*) as conta from descriçao where 
                                          Descriçao = @Descriçao";

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            int conta = 0;
            conta = Convert.ToInt32(dt.Rows[0]["conta"]);

            if (conta == 0)
                return false;
            else
                return true;
        }

        //obter linha de uma tabela do banco de acordo com um id passado, e jogando para um objeto
        public Models.Orçamento Obter(int id)
        {
            Models.Orçamento orçamento = null;

            string select = @"select * 
                              from orçamento 
                              where id = " + id;

            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                orçamento = Map(dt.Rows[0]);
            }

            return orçamento;

        }

        public List<Models.Orçamento> ObterTodos()
        {

            List<Models.Orçamento> dados = new List<Models.Orçamento>();

            try
            {
                string sql = @"select * from orçamento";
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Orçamento orçamento = new Models.Orçamento();
                    orçamento.clienteId = new Models.Cliente();
                    orçamento.setorId = new Models.Setor();

                    orçamento.Id = Convert.ToInt32(row["Id"]);
                    orçamento.Descriçao = row["Descriçao"].ToString();
                    orçamento.dtVencimento = row["DtVencimento"].ToString();
                    orçamento.clienteId.Id = Convert.ToInt32(row["Id_Cliente"]);
                    orçamento.setorId.Id = Convert.ToInt32(row["Id_Setor"]);
                    orçamento.FormaPag = row["FormaPag"].ToString();
                    orçamento.valorTotal = Convert.ToDouble(row["ValorTotal"]);

                    SetorDAL dals = new SetorDAL();
                    ClienteDAL dal = new ClienteDAL();
                    orçamento.setorId = dals.Obter(orçamento.setorId.Id);
                    orçamento.clienteId = dal.Obter(orçamento.clienteId.Id);

                    dados.Add(orçamento);
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

        public List<Models.Orçamento> BuscarOrçaCli(int id)
        {

            List<Models.Orçamento> dados = new List<Models.Orçamento>();

            try
            {
                string sql = @"select * from orçamento where Id_Cliente = " + id;
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Orçamento orçamento = new Models.Orçamento();
                    orçamento.clienteId = new Models.Cliente();
                    orçamento.setorId = new Models.Setor();

                    orçamento.Id = Convert.ToInt32(row["Id"]);
                    orçamento.Descriçao = row["Descriçao"].ToString();
                    orçamento.dtVencimento = row["DtVencimento"].ToString();
                    orçamento.clienteId.Id = Convert.ToInt32(row["Id_Cliente"]);
                    orçamento.setorId.Id = Convert.ToInt32(row["Id_Setor"]);
                    orçamento.FormaPag = row["FormaPag"].ToString();
                    orçamento.valorTotal = Convert.ToDouble(row["ValorTotal"]);

                    SetorDAL dals = new SetorDAL();
                    ClienteDAL dal = new ClienteDAL();
                    orçamento.setorId = dals.Obter(orçamento.setorId.Id);
                    orçamento.clienteId = dal.Obter(orçamento.clienteId.Id);

                    dados.Add(orçamento);
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
        public List<Models.Orçamento> Pesquisar(string descriçao)
        {

            List<Models.Orçamento> orçamentos = new List<Models.Orçamento>();

            string select = @"select * 
                              from orçamento 
                              where descriçao like @Descriçao";

            var parametros = _bd.GerarParametros();
            parametros.Add("@Descriçao", "%" + descriçao + "%");

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                orçamentos.Add(Map(row));
            }

            return orçamentos;

        }

        //faz o mapeamento, jogando oq tem na linha do banco em um objeto
        internal Models.Orçamento Map(DataRow row)
        {
            Models.Orçamento orçamento = new Models.Orçamento();
            orçamento.clienteId = new Models.Cliente();
            orçamento.setorId = new Models.Setor();

            orçamento.Id = Convert.ToInt32(row["Id"]);
            orçamento.Descriçao = row["Descriçao"].ToString();
            orçamento.dtVencimento = row["DtVencimento"].ToString();
            orçamento.clienteId.Id = Convert.ToInt32(row["Id_Cliente"]);
            orçamento.setorId.Id = Convert.ToInt32(row["Id_Setor"]);
            orçamento.FormaPag = row["FormaPag"].ToString();
            orçamento.valorTotal = Convert.ToInt32(row["ValorTotal"]);

            ClienteDAL dal = new ClienteDAL();
            orçamento.clienteId = dal.Obter(orçamento.clienteId.Id);
            SetorDAL dals = new SetorDAL();
            orçamento.setorId = dals.Obter(orçamento.setorId.Id);


            return orçamento;
        }


        //Excluir uma linha da tabela passando o seu id
        public bool Excluir(int id)
        {
            string select = @"delete 
                              from orçamento 
                              where id = " + id;

            return _bd.ExecutarNonQuery(select) > 0;

        }

        //Editar uma linha da tabela passando um objeto
        public bool Editar(Models.Orçamento orçamento)
        {
            int linhasAfetadas = 0;
            try
            {
                string update = @"update orçamento set Descriçao = @Descriçao, DtVencimento = @DtVencimento, Id_Cliente = @Id_Cliente, Id_Setor = @Id_Setor, FormaPag = @FormaPag, ValorTotal = @ValorTotal where Id =" + orçamento.Id;
                
                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Descriçao", orçamento.Descriçao);
                parametros.Add("@DtVencimento", orçamento.dtVencimento);
                parametros.Add("@Id_Cliente", orçamento.clienteId.Id);
                parametros.Add("@Id_Setor", orçamento.setorId.Id);
                parametros.Add("@FormaPag", orçamento.FormaPag);
                parametros.Add("@ValorTotal", orçamento.valorTotal);

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
