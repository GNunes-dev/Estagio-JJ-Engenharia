using HelloWorld.DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace Estagio.DAL
{
    public class ClienteDAL
    {
        MySQLPersistencia _bd = new MySQLPersistencia();

        //Cadastrar um Novo Cliente
        public bool Criar(Models.Cliente cliente)
        {
            //mapeamento Objeto-Relacional (ORM);
            int linhasAfetadas = 0;
            try
            {
                string insert = @"insert into cliente (Login, Senha, Email, Nome, CPF, Telefone, RG, Endereco, Bairro, CEP, id_cid, id_est)
                              values(@login, @senha,  @email, @nome,  @cpf, @telefone, @rg, @endereco, @bairro, @cep, @id_cid, @id_est)";

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@login", cliente.Login);
                parametros.Add("@senha", cliente.Senha);
                parametros.Add("@nome", cliente.Nome);
                parametros.Add("@email", cliente.Email);
                parametros.Add("@cpf", cliente.Cpf);
                parametros.Add("@telefone", cliente.Telefone);
                parametros.Add("@rg", cliente.Rg);
                parametros.Add("@endereco", cliente.Endereco);
                parametros.Add("@bairro", cliente.Bairro);
                parametros.Add("@cep", cliente.Cep);
                parametros.Add("@id_cid", cliente.CidadeId);
                parametros.Add("@id_est", cliente.EstadoId);

                linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
                if (linhasAfetadas > 0)
                {
                    cliente.Id = _bd.UltimoId;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }

        //Validar se uma conta é valida ao logar
        public Models.Cliente Validar(string login, string senha)
        {
            Models.Cliente usuarioRetorno = null;

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@login", login);
            parametros.Add("@senha", senha);

            string select = @"select id
                              from cliente 
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

            string select = @"select count(*) as conta from cliente where 
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
        public Models.Cliente Obter(int id)
        {
            Models.Cliente cliente = null;

            string select = @"select * 
                              from cliente 
                              where id = " + id;

            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                cliente = Map(dt.Rows[0]);
            }

            return cliente;

        }

        //obter linha de uma tabela do banco de acordo com um nome passado, e jogando para um objeto
        public List<Models.Cliente> Pesquisar(string nome)
        {

            List<Models.Cliente> clientes = new List<Models.Cliente>();

            string select = @"select * 
                              from cliente 
                              where nome like @nome";

            var parametros = _bd.GerarParametros();
            parametros.Add("@nome", "%" + nome + "%");

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                clientes.Add(Map(row));
            }

            return clientes;

        }

        //faz o mapeamento, jogando oq tem na linha do banco em um objeto
        internal Models.Cliente Map(DataRow row)
        {
            Models.Cliente cliente = new Models.Cliente();
            cliente.Id = Convert.ToInt32(row["id"]);
            cliente.Nome = row["nome"].ToString();
            cliente.Senha = row["senha"].ToString();
            cliente.Login = row["login"].ToString();
            cliente.Email = row["email"].ToString();
            cliente.Cpf = row["cpf"].ToString();
            cliente.Telefone = row["telefone"].ToString();
            cliente.Rg = row["rg"].ToString();
            cliente.Endereco = row["endereco"].ToString();
            cliente.Bairro = row["bairro"].ToString();
            cliente.Cep = row["cep"].ToString();
            cliente.EstadoId = Convert.ToInt32(row["Id_Est"]);
            cliente.CidadeId = Convert.ToInt32(row["Id_Cid"]);
            return cliente;
        }

        //Excluir uma linha da tabela passando o seu id
        public bool Excluir(int id)
        {
            string select = @"delete 
                              from cliente 
                              where id = " + id;

            return _bd.ExecutarNonQuery(select) > 0;

        }

        //Editar uma linha da tabela passando um objeto
        public bool Editar(Models.Cliente cliente)
        {
            int linhasAfetadas = 0;
            try
            {
                string update = @"update cliente set Login = @login, Senha = @senha, Email = @email, Nome = @nome, CPF = @cpf, Telefone = @telefone, RG = @rg, Endereco = @endereco, Bairro = @bairro, CEP = @cep, id_cid = @id_cid, id_est = @id_est where Id =" + cliente.Id;

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@login", cliente.Login);
                parametros.Add("@senha", cliente.Senha);
                parametros.Add("@nome", cliente.Nome);
                parametros.Add("@email", cliente.Email);
                parametros.Add("@cpf", cliente.Cpf);
                parametros.Add("@telefone", cliente.Telefone);
                parametros.Add("@rg", cliente.Rg);
                parametros.Add("@endereco", cliente.Endereco);
                parametros.Add("@bairro", cliente.Bairro);
                parametros.Add("@cep", cliente.Cep);
                parametros.Add("@id_cid", cliente.CidadeId);
                parametros.Add("@id_est", cliente.EstadoId);

                linhasAfetadas = _bd.ExecutarNonQuery(update, parametros);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }


        public bool EditarCli(Models.Cliente cliente)
        {
            int linhasAfetadas = 0;
            try
            {
                string update = @"update cliente set Login = @login, Senha = @senha, Email = @email, Nome = @nome, CPF = @cpf, Telefone = @telefone, RG = @rg, Endereco = @endereco, Bairro = @bairro, CEP = @cep, id_cid = @id_cid, id_est = @id_est where Id =" + cliente.Id;

                //var parametros = _bd.GerarParametros();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@login", cliente.Login);
                parametros.Add("@senha", cliente.Senha);
                parametros.Add("@nome", cliente.Nome);
                parametros.Add("@email", cliente.Email);
                parametros.Add("@cpf", cliente.Cpf);
                parametros.Add("@telefone", cliente.Telefone);
                parametros.Add("@rg", cliente.Rg);
                parametros.Add("@endereco", cliente.Endereco);
                parametros.Add("@bairro", cliente.Bairro);
                parametros.Add("@cep", cliente.Cep);
                parametros.Add("@id_cid", cliente.CidadeId);
                parametros.Add("@id_est", cliente.EstadoId);

                linhasAfetadas = _bd.ExecutarNonQuery(update, parametros);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return linhasAfetadas > 0;
        }

        public List<Models.Cliente> ObterTodos()
        {

            List<Models.Cliente> dados = new List<Models.Cliente>();

            try
            {
                string sql = @"select * from cliente";
                DataTable dt = _bd.ExecutarSelect(sql);
                foreach (DataRow row in dt.Rows)
                {
                    Models.Cliente cliente = new Models.Cliente();
                    cliente.Id = Convert.ToInt32(row["id"]);
                    cliente.Nome = row["nome"].ToString();
                    cliente.Senha = row["senha"].ToString();
                    cliente.Login = row["login"].ToString();
                    cliente.Email = row["email"].ToString();
                    cliente.Cpf = row["cpf"].ToString();
                    cliente.Telefone = row["telefone"].ToString();
                    cliente.Rg = row["rg"].ToString();
                    cliente.Endereco = row["endereco"].ToString();
                    cliente.Bairro = row["bairro"].ToString();
                    cliente.Cep = row["cep"].ToString();
                    cliente.EstadoId = Convert.ToInt32(row["Id_Est"]);
                    cliente.CidadeId = Convert.ToInt32(row["Id_Cid"]);
                    dados.Add(cliente);
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
    }
}
