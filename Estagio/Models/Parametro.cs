namespace Estagio.Models
{
    public class Parametro
    {
        private int _id;
        private string _razao;
        private string _nomefantasia;
        private string _cnpj;
        private string _ie;
        private string _datainicio;
        private string _endereco;
        private string _bairro;
        private string _cep;
        private int _cidadeid;
        private int _estadoid;
        private string _email;
        private string _telefone;

        public int Id { get => _id; set => _id = value; }
        public string Razao { get => _razao; set => _razao = value; }
        public string NomeFantasia { get => _nomefantasia; set => _nomefantasia = value; }
        public string Cnpj { get => _cnpj; set => _cnpj = value; }
        public string Ie { get => _ie; set => _ie = value; }
        public string DataInicio { get => _datainicio; set => _datainicio = value; }
        public string Telefone { get => _telefone; set => _telefone = value; }
        public string Email { get => _email; set => _email = value; }
        public string Endereco { get => _endereco; set => _endereco = value; }
        public string Cep { get => _cep; set => _cep = value; }
        public int CidadeId { get => _cidadeid; set => _cidadeid = value; }
        public int EstadoId { get => _estadoid; set => _estadoid = value; }
        public string Bairro { get => _bairro; set => _bairro = value; }

        public Parametro()
        {
            this._id = 0;
            this._razao = "";
            this._nomefantasia = "";
            this._cnpj = "";
            this._email = "";
            this._ie = "";
            this._datainicio = "";
            this._telefone = "";
            this._endereco = "";
            this._cep = "";
            this._cidadeid = 0;
            this._estadoid = 0;
            this._bairro = "";
        }

        public Parametro(int id, string razao, string nomefantasia, string cnpj, string ie, string email, string datainicio, string telefone, string bairro, string endereco, string cep, int cidadeid, int estadoid)
        {
            _id = id;
            _razao = razao;
            _nomefantasia = nomefantasia;
            _cnpj = cnpj;
            _ie = ie;
            _email = email;
            _datainicio = datainicio;
            _telefone = telefone;
            _bairro = bairro;
            _endereco = endereco;
            _cep = cep;
            _cidadeid = cidadeid;
            _estadoid = estadoid;
        }
    }
}
