using HelloWorld.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Estagio.DAL
{
    public class ProjetoDAL
    {
        MySQLPersistencia _bd = new MySQLPersistencia();//chamando banco de dados estabelecido na classe MYSQLPersistencia

        //Cadastrar uma nova licença pegando informações de cliente e orgao de licenciamento
        public int Criar(Models.Projeto projeto)
        {
            //mapeamento Objeto-Relacional (ORM);
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into projeto (Cliente_Id, Setor_Id, Funcionario_Id, FormaPag, Descriçao, DtInicial, DtPrevFinal, DtFinal, Endereço, Bairro, Cep, Estado_Id, Cidade_Id, ValorTotal, Status)
                              values(@Cliente_Id, @Setor_Id, @Funcionario_Id, @FormaPag, @Descriçao, @DtInicial, @DtPrevFinal, @DtFinal, @Endereço, @Bairro, @Cep, @Estado_Id, @Cidade_Id, @ValorTotal, @Status)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Cliente_Id", projeto.Cliente.Id);
                parametros.Add("@Setor_Id", projeto.Setor.Id);
                parametros.Add("@Funcionario_Id", projeto.Funcionario.Id);
                parametros.Add("@FormaPag", projeto.FormaPag);
                parametros.Add("@Descriçao", projeto.Descriçao);
                parametros.Add("@DtInicial", projeto.dtInicial);
                parametros.Add("@DtPrevFinal", projeto.dtPrevFinal);
                parametros.Add("@DtFinal", projeto.dtFinal);
                parametros.Add("@Endereço", projeto.Endereco);
                parametros.Add("@Bairro", projeto.Bairro);
                parametros.Add("@Cep", projeto.Cep);
                parametros.Add("@Estado_Id", projeto.Estado.Id);
                parametros.Add("@Cidade_Id", projeto.Cidade.Id);
                parametros.Add("@ValorTotal", projeto.valorTotal);
                parametros.Add("@Status", projeto.Status);

                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
                if (linhasAfetadas > 0)
                {
                    projeto.Id = _bd.UltimoId;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            if (linhasAfetadas > 0)
                return projeto.Id;
            else
                return -1;

        }

        //grava a lista de serviço em orçamento
        public bool CriarProjetoServiço(int id, int idproj)
        {
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into projeto_serviço (Id_projeto, Id_servico)
                              values(@Id_projeto, @Id_servico)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Id_projeto", idproj);
                parametros.Add("@Id_servico", id);

                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }

        public List<Models.Serviço> BuscarProjetoServiço(int id)
        {
            List<Models.ProjServ> dados = new List<Models.ProjServ>();
            List<Models.Serviço> serv = new List<Models.Serviço>();

            DAL.ServiçoDAL sbd = new DAL.ServiçoDAL();
            try
            {
                string sql = @"select * from projeto_serviço
                              where Id_projeto =" + id;

                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.ProjServ itens = new Models.ProjServ();

                    itens.Id_proj = Convert.ToInt32(row["Id_projeto"]);
                    itens.Id_serv = Convert.ToInt32(row["Id_servico"]);

                    serv.Add(sbd.Obter(itens.Id_serv));
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
        public bool CriarProjetoLicença(int id, int idproj)
        {
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into projeto_licença (Id_projeto, Id_licenca)
                              values(@Id_projeto, @Id_licenca)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Id_projeto", idproj);
                parametros.Add("@Id_licenca", id);

                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }

        public List<Models.Licença> BuscarProjetoLicença(int id)
        {
            List<Models.ProjLic> dados = new List<Models.ProjLic>();
            List<Models.Licença> lic = new List<Models.Licença>();

            DAL.LicençaDAL sbd = new DAL.LicençaDAL();
            try
            {
                string sql = @"select * from projeto_licença
                              where Id_projeto =" + id;

                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.ProjLic itens = new Models.ProjLic();

                    itens.Id_proj = Convert.ToInt32(row["Id_projeto"]);
                    itens.Id_lic = Convert.ToInt32(row["Id_licenca"]);

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

        public List<Models.Projeto> ObterTodos()
        {

            List<Models.Projeto> dados = new List<Models.Projeto>();

            try
            {
                string sql = @"select * from projeto";
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Projeto projeto = new Models.Projeto();
                    projeto.Cliente = new Models.Cliente();
                    projeto.Setor = new Models.Setor();
                    projeto.Cidade = new Models.Cidade();
                    projeto.Estado = new Models.Estado();
                    projeto.Funcionario = new Models.Funcionario();

                    projeto.Id = Convert.ToInt32(row["Id"]);
                    projeto.Cliente.Id = Convert.ToInt32(row["Cliente_Id"]);
                    projeto.Setor.Id = Convert.ToInt32(row["Setor_Id"]);
                    projeto.Funcionario.Id = Convert.ToInt32(row["Funcionario_Id"]);
                    projeto.FormaPag = row["FormaPag"].ToString();
                    projeto.Descriçao = row["Descriçao"].ToString();
                    projeto.dtInicial = row["DtInicial"].ToString();
                    projeto.dtPrevFinal = row["DtPrevFinal"].ToString();
                    projeto.dtFinal = row["DtFinal"].ToString();
                    projeto.Endereco = row["Endereço"].ToString();
                    projeto.Bairro = row["Bairro"].ToString();
                    projeto.Cep = row["Cep"].ToString();
                    projeto.Estado.Id = Convert.ToInt32(row["Estado_Id"]);
                    projeto.Cidade.Id = Convert.ToInt32(row["Cidade_Id"]);
                    projeto.valorTotal = Convert.ToDouble(row["ValorTotal"]);
                    projeto.Status = row["Status"].ToString();

                    ClienteDAL dal = new ClienteDAL();
                    projeto.Cliente = dal.Obter(projeto.Cliente.Id);

                    SetorDAL dals = new SetorDAL();
                    projeto.Setor = dals.Obter(projeto.Setor.Id);

                    FuncionarioDAL dalf = new FuncionarioDAL();
                    projeto.Funcionario = dalf.Obter(projeto.Funcionario.Id);

                    dados.Add(projeto);
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
                string update = @"update projeto set Status = @Status where Id =" + id;

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
        public Models.Projeto Obter(int id)
        {
            Models.Projeto projeto = null;

            string select = @"select * 
                              from projeto 
                              where id = " + id;

            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                projeto = Map(dt.Rows[0]);
            }

            return projeto;

        }

        public List<Models.Projeto> ObterCli(int id)
        {

            List<Models.Projeto> dados = new List<Models.Projeto>();

            try
            {
                string sql = @"select * from projeto where Cliente_Id =" + id;
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Projeto projeto = new Models.Projeto();
                    projeto.Cliente = new Models.Cliente();
                    projeto.Setor = new Models.Setor();
                    projeto.Cidade = new Models.Cidade();
                    projeto.Estado = new Models.Estado();
                    projeto.Funcionario = new Models.Funcionario();

                    projeto.Id = Convert.ToInt32(row["Id"]);
                    projeto.Cliente.Id = Convert.ToInt32(row["Cliente_Id"]);
                    projeto.Setor.Id = Convert.ToInt32(row["Setor_Id"]);
                    projeto.Funcionario.Id = Convert.ToInt32(row["Funcionario_Id"]);
                    projeto.FormaPag = row["FormaPag"].ToString();
                    projeto.Descriçao = row["Descriçao"].ToString();
                    projeto.dtInicial = row["DtInicial"].ToString();
                    projeto.dtPrevFinal = row["DtPrevFinal"].ToString();
                    projeto.dtFinal = row["DtFinal"].ToString();
                    projeto.Endereco = row["Endereço"].ToString();
                    projeto.Bairro = row["Bairro"].ToString();
                    projeto.Cep = row["Cep"].ToString();
                    projeto.Estado.Id = Convert.ToInt32(row["Estado_Id"]);
                    projeto.Cidade.Id = Convert.ToInt32(row["Cidade_Id"]);
                    projeto.valorTotal = Convert.ToDouble(row["ValorTotal"]);
                    projeto.Status = row["Status"].ToString();

                    ClienteDAL dal = new ClienteDAL();
                    projeto.Cliente = dal.Obter(projeto.Cliente.Id);

                    SetorDAL dals = new SetorDAL();
                    projeto.Setor = dals.Obter(projeto.Setor.Id);

                    FuncionarioDAL dalf = new FuncionarioDAL();
                    projeto.Funcionario = dalf.Obter(projeto.Funcionario.Id);

                    dados.Add(projeto);
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

        //faz o mapeamento, jogando oq tem na linha do banco em um objeto
        internal Models.Projeto Map(DataRow row)
        {

            Models.Projeto projeto = new Models.Projeto();
            projeto.Cliente = new Models.Cliente();
            projeto.Setor = new Models.Setor();
            projeto.Cidade = new Models.Cidade();
            projeto.Estado = new Models.Estado();
            projeto.Funcionario = new Models.Funcionario();

            projeto.Id = Convert.ToInt32(row["Id"]);
            projeto.Cliente.Id = Convert.ToInt32(row["Cliente_Id"]);
            projeto.Setor.Id = Convert.ToInt32(row["Setor_Id"]);
            projeto.Funcionario.Id = Convert.ToInt32(row["Funcionario_Id"]);
            projeto.FormaPag = row["FormaPag"].ToString();
            projeto.Descriçao = row["Descriçao"].ToString();
            projeto.dtInicial = row["DtInicial"].ToString();
            projeto.dtPrevFinal = row["DtPrevFinal"].ToString();
            projeto.dtFinal = row["DtFinal"].ToString();
            projeto.Endereco = row["Endereço"].ToString();
            projeto.Bairro = row["Bairro"].ToString();
            projeto.Cep = row["Cep"].ToString();
            projeto.Estado.Id = Convert.ToInt32(row["Estado_Id"]);
            projeto.Cidade.Id = Convert.ToInt32(row["Cidade_Id"]);
            projeto.valorTotal = Convert.ToDouble(row["ValorTotal"]);
            projeto.Status = row["Status"].ToString();

            ClienteDAL dal = new ClienteDAL();
            projeto.Cliente = dal.Obter(projeto.Cliente.Id);

            SetorDAL dals = new SetorDAL();
            projeto.Setor = dals.Obter(projeto.Setor.Id);

            FuncionarioDAL dalf = new FuncionarioDAL();
            projeto.Funcionario = dalf.Obter(projeto.Funcionario.Id);

            return projeto;
        }


        //Excluir uma linha da tabela passando o seu id
        public bool Excluir(int id)
        {
            string select = @"delete 
                              from projeto 
                              where id = " + id;

            return _bd.ExecutarNonQuery(select) > 0;

        }

    }
}
