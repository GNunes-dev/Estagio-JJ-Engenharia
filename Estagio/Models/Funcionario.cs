namespace Estagio.Models
{
    public class Funcionario
    {
        private int _id;
        private string _login;
        private string _senha;
        private string _nome;
        private string _email;
        private string _telefone;
        private string _cpf;
        private string _rg;
        private string _crea;
        private string _endereco;
        private string _bairro;
        private string _cep;
        private int _cidadeid;
        private int _estadoid;

        public int Id { get => _id; set => _id = value; }
        public string Login { get => _login; set => _login = value; }
        public string Senha { get => _senha; set => _senha = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Email { get => _email; set => _email = value; }
        public string Cpf { get => _cpf; set => _cpf = value; }
        public string Telefone { get => _telefone; set => _telefone = value; }
        public string Rg { get => _rg; set => _rg = value; }
        public string Crea { get => _crea; set => _crea = value; }
        public string Endereco { get => _endereco; set => _endereco = value; }
        public string Bairro { get => _bairro; set => _bairro = value; }
        public string Cep { get => _cep; set => _cep = value; }
        public int CidadeId { get => _cidadeid; set => _cidadeid = value; }
        public int EstadoId { get => _estadoid; set => _estadoid = value; }
        public Funcionario()
        {
            this._id = 0;
            this._login = "";
            this._senha = "";
            this._nome = "";
            this._email = "";
            this._cpf = "";
            this._rg = "";
            this._crea = "";
            this._telefone = "";
            this._endereco = "";
            this._bairro = "";
            this._cep = "";
            this._cidadeid = 0;
            this._estadoid = 0;
        }


        public Funcionario(int id, string login, string senha)
        {
            _id = id;
            _login = login;
            _senha = senha;
        }

        public Funcionario(int id, string login, string senha, string nome, string email, string cpf, string telefone, string rg, string crea, string endereco, string bairro, string cep, int cidadeid, int estadoid)
        {
            _id = id;
            _login = login;
            _senha = senha;
            _nome = nome;
            _email = email;
            _cpf = cpf;
            _telefone = telefone;
            _rg = rg;
            _crea = crea;
            _endereco = endereco;
            _bairro = bairro;
            _cep = cep;
            _cidadeid = cidadeid;
            _estadoid = estadoid;
        }

        public Funcionario BuscarFuncionario(int id)
        {
            if (id > 0)
                return new DAL.FuncionarioDAL().Obter(id);
            else
                return null;
        }
    }
}
