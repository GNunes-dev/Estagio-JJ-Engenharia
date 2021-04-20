using HelloWorld.DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace Estagio.DAL
{
    public class FuncionarioDAL
    {
        MySQLPersistencia _bd = new MySQLPersistencia(); //chamando banco de dados estabelecido na classe MYSQLPersistencia

        //Cadastrar um Novo Funcionario
        public bool Criar(Models.Funcionario funcionario)
        {
            //mapeamento Objeto-Relacional (ORM);
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into funcionario (Login, Senha, Email, Nome, CPF, Telefone, RG, Crea, Endereco, Bairro, CEP, Id_Cidade, Id_Estado)
                              values(@login, @senha,  @email, @nome,  @cpf, @telefone, @rg, @crea, @endereco, @bairro, @cep, @Id_Cidade, @Id_Estado)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@login", funcionario.Login);
                parametros.Add("@senha", funcionario.Senha);
                parametros.Add("@nome", funcionario.Nome);
                parametros.Add("@email", funcionario.Email);
                parametros.Add("@cpf", funcionario.Cpf);
                parametros.Add("@telefone", funcionario.Telefone);
                parametros.Add("@rg", funcionario.Rg);
                parametros.Add("@crea", funcionario.Crea);
                parametros.Add("@endereco", funcionario.Endereco);
                parametros.Add("@bairro", funcionario.Bairro);
                parametros.Add("@cep", funcionario.Cep);
                parametros.Add("@Id_Cidade", funcionario.CidadeId);
                parametros.Add("@Id_Estado", funcionario.EstadoId);

                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
                if (linhasAfetadas > 0)
                {
                    funcionario.Id = _bd.UltimoId;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }

        //Validar se uma conta é valida ao logar
        public Models.Funcionario Validar(string login, string senha)
        {

            Models.Funcionario usuarioRetorno = null;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@login", login);
            parametros.Add("@senha", senha);

            string select = @"select id
                              from funcionario 
                              where login = @login and 
                                    senha = @senha";

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            int conta = dt.Rows.Count;

            if (conta > 0)
            {
                int id = Convert.ToInt32(dt.Rows[0]["id"]);

                usuarioRetorno = Obter(id);
            }

            return usuarioRetorno;
        }

        //Validar se ao cadastrar não existe outro login com o mesmo nome
        public bool validarLoginUnico(string login)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@login", login);

            string select = @"select count(*) as conta from funcionario where 
                                          login = @login";

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            int conta = 0;
            conta = Convert.ToInt32(dt.Rows[0]["conta"]);

            if (conta == 0)
                return false;
            else
                return true;
        }

        //obter linha de uma tabela do banco de acordo com um id passado, e jogando para um objeto
        public Models.Funcionario Obter(int id)
        {
            Models.Funcionario funcionario = null;

            string select = @"select * 
                              from funcionario 
                              where id = " + id;

            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                funcionario = Map(dt.Rows[0]);
            }

            return funcionario;

        }

        public List<Models.Funcionario> ObterTodos()
        {

            List<Models.Funcionario> dados = new List<Models.Funcionario>();

            try
            {
                string sql = @"select * from funcionario";
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Funcionario funcionario = new Models.Funcionario();
                    funcionario.Id = Convert.ToInt32(row["id"]);
                    funcionario.Nome = row["nome"].ToString();
                    funcionario.Login = row["login"].ToString();
                    funcionario.Email = row["email"].ToString();
                    funcionario.Cpf = row["cpf"].ToString();
                    funcionario.Telefone = row["telefone"].ToString();
                    funcionario.Rg = row["rg"].ToString();
                    funcionario.Crea = row["crea"].ToString();
                    funcionario.Endereco = row["endereco"].ToString();
                    funcionario.Bairro = row["bairro"].ToString();
                    funcionario.Cep = row["cep"].ToString();
                    funcionario.CidadeId = Convert.ToInt32(row["Id_Cidade"]);
                    funcionario.EstadoId = Convert.ToInt32(row["Id_Estado"]);
                    dados.Add(funcionario);
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
        public List<Models.Funcionario> Pesquisar(string nome)
        {

            List<Models.Funcionario> funcionarios = new List<Models.Funcionario>();

            string select = @"select * 
                              from funcionario 
                              where nome like @nome";

            var parametros = _bd.GerarParametros();
            parametros.Add("@nome", "%" + nome + "%");

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                funcionarios.Add(Map(row));
            }

            return funcionarios;

        }

        //faz o mapeamento, jogando oq tem na linha do banco em um objeto
        internal Models.Funcionario Map(DataRow row)
        {
            Models.Funcionario funcionario = new Models.Funcionario();
            funcionario.Id = Convert.ToInt32(row["id"]);
            funcionario.Nome = row["nome"].ToString();
            funcionario.Senha = row["senha"].ToString();
            funcionario.Login = row["login"].ToString();
            funcionario.Email = row["email"].ToString();
            funcionario.Cpf = row["cpf"].ToString();
            funcionario.Telefone = row["telefone"].ToString();
            funcionario.Rg = row["rg"].ToString();
            funcionario.Crea = row["crea"].ToString();
            funcionario.Endereco = row["endereco"].ToString();
            funcionario.Bairro = row["bairro"].ToString();
            funcionario.Cep = row["cep"].ToString();
            funcionario.CidadeId = Convert.ToInt32(row["Id_Cidade"]);
            funcionario.EstadoId = Convert.ToInt32(row["Id_Estado"]);
            return funcionario;
        }

        //Excluir uma linha da tabela passando o seu id
        public bool Excluir(int id)
        {
            string select = @"delete 
                              from funcionario 
                              where id = " + id;

            return _bd.ExecutarNonQuery(select) > 0;

        }

        //Editar uma linha da tabela passando um objeto
        public bool Editar(Models.Funcionario funcionario)
        {
            int linhasAfetadas = 0;
            try
            {
                string update = @"update funcionario set Login = @login, Senha = @senha, Email = @email, Nome = @nome, CPF = @cpf, Telefone = @telefone, RG = @rg, Crea = @crea, Endereco = @endereco, Bairro = @bairro, CEP = @cep, Id_Cidade = @Id_Cidade, Id_Estado = @Id_Estado where Id =" + funcionario.Id;

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@login", funcionario.Login);
                parametros.Add("@senha", funcionario.Senha);
                parametros.Add("@nome", funcionario.Nome);
                parametros.Add("@email", funcionario.Email);
                parametros.Add("@cpf", funcionario.Cpf);
                parametros.Add("@telefone", funcionario.Telefone);
                parametros.Add("@rg", funcionario.Rg);
                parametros.Add("@crea", funcionario.Crea);
                parametros.Add("@endereco", funcionario.Endereco);
                parametros.Add("@bairro", funcionario.Bairro);
                parametros.Add("@cep", funcionario.Cep);
                parametros.Add("@Id_Cidade", funcionario.CidadeId);
                parametros.Add("@Id_Estado", funcionario.EstadoId);

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
