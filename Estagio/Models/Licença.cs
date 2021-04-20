namespace Estagio.Models
{
    public class Licença
    {
        private int _id;
        private string _nome;
        private string _dtVencimento;
        private Cliente _clienteid;
        private int _orgaoid;
        private int _setorid;
        private double _valorTotal;
        private string _dtInicial;
        private string _numProcesso;
        private string _numLicença;
        private string _versao;
        private Funcionario _funcionario;
        private string _cnae;

        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string dtVencimento { get => _dtVencimento; set => _dtVencimento = value; }
        public Cliente clienteId { get => _clienteid; set => _clienteid = value; }
        public int orgaoId { get => _orgaoid; set => _orgaoid = value; }
        public int setorId { get => _setorid; set => _setorid = value; }
        public double valorTotal { get => _valorTotal; set => _valorTotal = value; }
        public string dtInicial { get => _dtInicial; set => _dtInicial = value; }
        public string numProcesso { get => _numProcesso; set => _numProcesso = value; }
        public string numLicença { get => _numLicença; set => _numLicença = value; }
        public string Versao { get => _versao; set => _versao = value; }
        public Funcionario Funcionario { get => _funcionario; set => _funcionario = value; }
        public string Cnae { get => _cnae; set => _cnae = value; }

        public Licença()
        {
            this._id = 0;
            this._nome = "";
            this._dtVencimento = "";
            this._clienteid = null;
            this._orgaoid = 0;
            this.setorId = 0;
            this.valorTotal = 0;
            this._dtInicial = "";
            this._numProcesso = "";
            this._numLicença = "";
            this._versao = "";
            this._funcionario = null;
            this._cnae = "";
        }

        public Licença(int id, string nome, string dtvencimento, Cliente clienteid, int orgaoid, int setorid, double valorTotal, string dtinicial, string numprocesso, string numlicença, string versao, Funcionario funcionario, string cnae)
        {
            _id = id;
            _nome = nome;
            _dtVencimento = dtvencimento;
            _clienteid = clienteid;
            _orgaoid = orgaoid;
            _setorid = setorid;
            _valorTotal = valorTotal;
            _dtInicial = dtinicial;
            _numProcesso = numprocesso;
            _numLicença = numlicença;
            _versao = versao;
            _funcionario = funcionario;
            _cnae = cnae;
        }

        public Licença BuscarLicença(int id)
        {
            if (id > 0)
                return new DAL.LicençaDAL().Obter(id);
            else
                return null;
        }
    }
}
