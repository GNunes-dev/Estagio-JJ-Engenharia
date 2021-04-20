using HelloWorld.DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace Estagio.DAL
{
    public class LicençaDAL
    {
        MySQLPersistencia _bd = new MySQLPersistencia();//chamando banco de dados estabelecido na classe MYSQLPersistencia

        //Cadastrar uma nova licença pegando informações de cliente e orgao de licenciamento
        public bool Criar(Models.Licença licença)
        {
            //mapeamento Objeto-Relacional (ORM);
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into licença (Nome, DtVencimento, Id_Cli, Id_Org, SetorId, ValorTotal, DtInicial, Cnae, Id_Fun, NumProcesso, NumLicença, Versao)
                              values(@Nome, @DtVencimento, @Id_Cli, @Id_Org, @SetorId, @ValorTotal, @DtInicial, @Cnae, @Id_Fun, @NumProcesso, @NumLicença, @Versao)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Nome", licença.Nome);
                parametros.Add("@DtVencimento", licença.dtVencimento);
                parametros.Add("@Id_Cli", licença.clienteId.Id);
                parametros.Add("@Id_Org", licença.orgaoId);
                parametros.Add("@SetorId", licença.setorId);
                parametros.Add("@ValorTotal", licença.valorTotal);
                parametros.Add("@DtInicial", licença.dtInicial);
                parametros.Add("@Cnae", licença.Cnae);
                parametros.Add("@Id_Fun", licença.Funcionario.Id);
                parametros.Add("@NumProcesso", licença.numProcesso);
                parametros.Add("@NumLicença", licença.numLicença);
                parametros.Add("@Versao", licença.Versao);

                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
                if (linhasAfetadas > 0)
                {
                    licença.Id = _bd.UltimoId;
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
            parametros.Add("@Nome", nome);

            string select = @"select count(*) as conta from licença where 
                                          Nome = @Nome";

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            int conta = 0;
            conta = Convert.ToInt32(dt.Rows[0]["conta"]);

            if (conta == 0)
                return false;
            else
                return true;
        }

        //obter linha de uma tabela do banco de acordo com um id passado, e jogando para um objeto
        public Models.Licença Obter(int id)
        {
            Models.Licença licença = null;

            string select = @"select * 
                              from licença 
                              where id = " + id;

            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                licença = Map(dt.Rows[0]);
            }

            return licença;

        }

        //obter linha de uma tabela do banco de acordo com um nome passado, e jogando para um objeto
        public List<Models.Licença> Pesquisar(string nome)
        {

            List<Models.Licença> licenças = new List<Models.Licença>();

            string select = @"select * 
                              from licença 
                              where nome like @Nome";

            var parametros = _bd.GerarParametros();
            parametros.Add("@Nome", "%" + nome + "%");

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                licenças.Add(Map(row));
            }

            return licenças;

        }

        public List<Models.Licença> BuscarLicençaCli(int id)
        {

            List<Models.Licença> dados = new List<Models.Licença>();

            try
            {
                string sql = @"select * from licença where Id_Cli =" + id;
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Licença licença = new Models.Licença();
                    licença.clienteId = new Models.Cliente();
                    licença.Funcionario = new Models.Funcionario();

                    licença.Id = Convert.ToInt32(row["Id"]);
                    licença.Nome = row["Nome"].ToString();
                    licença.dtVencimento = row["DtVencimento"].ToString();
                    licença.clienteId.Id = Convert.ToInt32(row["Id_Cli"]);
                    licença.orgaoId = Convert.ToInt32(row["Id_Org"]);
                    licença.setorId = Convert.ToInt32(row["SetorId"]);
                    licença.valorTotal = Convert.ToDouble(row["ValorTotal"]);
                    licença.dtInicial = row["DtInicial"].ToString();
                    licença.Cnae = row["Cnae"].ToString();
                    licença.Funcionario.Id = Convert.ToInt32(row["Id_Fun"]);
                    licença.numProcesso = row["NumProcesso"].ToString();
                    licença.numLicença = row["NumLicença"].ToString();
                    licença.Versao = row["Versao"].ToString();
                    FuncionarioDAL dalf = new FuncionarioDAL();
                    ClienteDAL dal = new ClienteDAL();
                    licença.Funcionario = dalf.Obter(licença.Funcionario.Id);
                    licença.clienteId = dal.Obter(licença.clienteId.Id);

                    dados.Add(licença);
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

        public List<Models.Licença> ObterTodos()
        {

            List<Models.Licença> dados = new List<Models.Licença>();

            try
            {
                string sql = @"select * from licença";
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Licença licença = new Models.Licença();
                    licença.clienteId = new Models.Cliente();
                    licença.Funcionario = new Models.Funcionario();

                    licença.Id = Convert.ToInt32(row["Id"]);
                    licença.Nome = row["Nome"].ToString();
                    licença.dtVencimento = row["DtVencimento"].ToString();
                    licença.clienteId.Id = Convert.ToInt32(row["Id_Cli"]);
                    licença.orgaoId = Convert.ToInt32(row["Id_Org"]);
                    licença.setorId = Convert.ToInt32(row["SetorId"]);
                    licença.valorTotal = Convert.ToDouble(row["ValorTotal"]);
                    licença.dtInicial = row["DtInicial"].ToString();
                    licença.Cnae = row["Cnae"].ToString();
                    licença.Funcionario.Id = Convert.ToInt32(row["Id_Fun"]);
                    licença.numProcesso = row["NumProcesso"].ToString();
                    licença.numLicença = row["NumLicença"].ToString();
                    licença.Versao = row["Versao"].ToString();
                    FuncionarioDAL dalf = new FuncionarioDAL();
                    ClienteDAL dal = new ClienteDAL();
                    licença.Funcionario = dalf.Obter(licença.Funcionario.Id);
                    licença.clienteId = dal.Obter(licença.clienteId.Id);

                    dados.Add(licença);
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
        internal Models.Licença Map(DataRow row)
        {
            Models.Licença licença = new Models.Licença();
            licença.clienteId = new Models.Cliente();
            licença.Funcionario = new Models.Funcionario();

            licença.Id = Convert.ToInt32(row["Id"]);
            licença.Nome = row["Nome"].ToString();
            licença.dtVencimento = row["DtVencimento"].ToString();
            licença.clienteId.Id = Convert.ToInt32(row["Id_Cli"]);
            licença.orgaoId = Convert.ToInt32(row["Id_Org"]);
            licença.setorId = Convert.ToInt32(row["SetorId"]);
            licença.valorTotal = Convert.ToDouble(row["ValorTotal"]);
            licença.dtInicial = row["DtInicial"].ToString();
            licença.Cnae = row["Cnae"].ToString();
            licença.Funcionario.Id = Convert.ToInt32(row["Id_Fun"]);
            licença.numProcesso = row["NumProcesso"].ToString();
            licença.numLicença = row["NumLicença"].ToString();
            licença.Versao = row["Versao"].ToString();

            FuncionarioDAL df = new FuncionarioDAL();
            ClienteDAL dal = new ClienteDAL();
            licença.Funcionario = df.Obter(licença.Funcionario.Id);
            licença.clienteId = dal.Obter(licença.clienteId.Id);

            return licença;
        }

        //Excluir uma linha da tabela passando o seu id
        public bool Excluir(int id)
        {
            string select = @"delete 
                              from licença 
                              where id = " + id;

            return _bd.ExecutarNonQuery(select) > 0;

        }

        public bool Att(int id, string Versao, string dtVenc)
        {
            int linhasAfetadas = 0;
            try
            {
                string update = @"update licença set DtVencimento = @DtVencimento, Versao = @Versao where Id =" + id;

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@Versao", Versao);
                parametros.Add("@DtVencimento", dtVenc);

                linhasAfetadas = _bd.ExecutarNonQuery(update, parametros);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }


        public bool AtualizarDoc(Models.Licença_Documento licD)
        {
            int linhasAfetadas = 0;

            if (licD != null)
            {
                string sql = @"INSERT INTO licença_documento(Id_lic,nome,formato,type,tamanho,arquivo)
                            VALUES(@Id_lic,@nome,@formato,@type,@tamanho,@arquivo); ";

                Dictionary<string, object> ps = new Dictionary<string, object>();
                ps.Add("@Id_lic", licD.IdLic);
                ps.Add("@nome", licD.Nome);
                ps.Add("@formato", licD.Formato);
                ps.Add("@type", licD.Type);
                ps.Add("@tamanho", licD.Tamanho);

                Dictionary<string, byte[]> psB = new Dictionary<string, byte[]>();
                psB.Add("@arquivo", licD.Arq);
                linhasAfetadas = _bd.Executar(sql, ps, psB);
                licD.Id = (int)_bd.UltimoId;

                return linhasAfetadas > 0;
            }
            else
                return false;
           
        }

        public Models.Licença_Documento GetArquivo(int id)
        {
            Models.Licença_Documento licD = new Models.Licença_Documento();
            string sql = "select Arquivo from licença_documento where Id_lic = " + id;
            object arq = _bd.ExecutarScalar(sql);

            if (arq != null)
            {
                string type, nome;
                (type, nome) = GetInfoDoc(id);
                licD.Arq = (byte[])arq;
                licD.Type = type;
                licD.Nome = nome;
                _bd.Fechar();
                return licD;
            }
            else
                return null;
        }

        internal (string, string) GetInfoDoc(int id)
        {
            string sql = "select type,nome from licença_documento where Id_lic = " + id;
            DataTable dt = _bd.ExecutarSelect(sql);
            _bd.Fechar();
            try
            {
                return(dt.Rows[0]["type"].ToString(), dt.Rows[0]["nome"].ToString());
            }
            catch
            {
                return (null, null);
            }
               
        }

    }
}
