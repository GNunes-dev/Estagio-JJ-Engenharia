namespace Estagio.Models
{
    public class GerarServiço
    {
        private int _id;
        private Cliente _clienteid;
        private int _funcionarioid;
        private string _descriçao;
        private string _dtInicio;
        private string _dtPrevFim;
        private string _dtFim;
        private string _endereco;
        private string _bairro;
        private string _cep;
        private int _cidadeid;
        private int _estadoid;
        private int _setorid;
        private int _serviçoid;
        private double _valorTotal;
        private string _status;

        public int Id { get => _id; set => _id = value; }
        public int funcionarioiId { get => _funcionarioid; set => _funcionarioid = value; }
        public Cliente clienteId { get => _clienteid; set => _clienteid = value; }
        public string Descriçao { get => _descriçao; set => _descriçao = value; }
        public string dtInicio { get => _dtInicio; set => _dtInicio = value; }
        public string dtPrevFim { get => _dtPrevFim; set => _dtPrevFim = value; }
        public string dtFim { get => _dtFim; set => _dtFim = value; }
        public string Endereco { get => _endereco; set => _endereco = value; }
        public string Bairro { get => _bairro; set => _bairro = value; }
        public string Cep { get => _cep; set => _cep = value; }
        public int cidadeId { get => _cidadeid; set => _cidadeid = value; }
        public int estadoId { get => _estadoid; set => _estadoid = value; }
        public int serviçoId { get => _serviçoid; set => _serviçoid = value; }
        public int setorId { get => _setorid; set => _setorid = value; }
        public double ValorTotal { get => _valorTotal; set => _valorTotal = value; }
        public string Status { get => _status; set => _status = value; }

        public GerarServiço()
        {
            this._id = 0;
            this._clienteid = null;
            this.funcionarioiId = 0;
            this._descriçao = "";
            this._dtInicio = "";
            this._dtPrevFim = "";
            this._dtFim = "";
            this._endereco = "";
            this._bairro = "";
            this._cep = "";
            this._cidadeid = 0;
            this._estadoid = 0;
            this._setorid = 0;
            this._serviçoid = 0;
            this._valorTotal = 0;
            this._status = "";
        }

        public GerarServiço(int id, Cliente clienteid, int funcionarioid, string descriçao, string dtInicio, string dtPrevFim, string dtFim, string endereco, string bairro, string cep, int cidadeid, int estadoid, int setorid, int serviçoid, double valorTotal, string status)
        {
            _id = id;
            _clienteid = clienteid;
            _funcionarioid = funcionarioid;
            _descriçao = descriçao;
            _dtInicio = dtInicio;
            _dtPrevFim = dtPrevFim;
            _dtFim = dtFim;
            _endereco = endereco;
            _bairro = bairro;
            _cep = cep;
            _cidadeid = cidadeid;
            _estadoid = estadoid;
            _setorid = setorid;
            _serviçoid = serviçoid;
            _valorTotal = valorTotal;
            _status = status;
        }

        public GerarServiço(int id, int serviçoid)
        {
            _id = id;
            _serviçoid = serviçoid;
        }

        public GerarServiço BuscarServiço(int id)
        {
            if (id > 0)
                return new DAL.GerarServiçoDAL().Obter(id);
            else
                return null;
        }
    }
}
