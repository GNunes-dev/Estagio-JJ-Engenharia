namespace Estagio.Models
{
    public class Orçamento
    {
        private int _id;
        private string _descriçao;
        private string _dtVencimento;
        private Cliente _clienteid;
        private Setor _setorid;
        private string _formapag;
        private int _serviçoid;
        private double _valorTotal;

        public int Id { get => _id; set => _id = value; }
        public string Descriçao { get => _descriçao; set => _descriçao = value; }
        public string dtVencimento { get => _dtVencimento; set => _dtVencimento = value; }
        public Cliente clienteId { get => _clienteid; set => _clienteid = value; }
        public Setor setorId { get => _setorid; set => _setorid = value; }
        public string FormaPag { get => _formapag; set => _formapag = value; }
        public int serviçoId { get => _serviçoid; set => _serviçoid = value; }
        public double valorTotal { get => _valorTotal; set => _valorTotal = value; }

        public Orçamento()
        {
            this._id = 0;
            this._descriçao = "";
            this._dtVencimento = "";
            this._clienteid = null;
            this._setorid = null;
            this._formapag = "";
            this._serviçoid = 0;
            this._valorTotal = 0;
        }

        public Orçamento(int id, string descriçao, string dtvencimento, Cliente clienteid, Setor setorid, string formapag, int serviçoid, double valortotal)
        {
            _id = id;
            _descriçao = descriçao;
            _dtVencimento = dtvencimento;
            _clienteid = clienteid;
            _setorid = setorid;
            _formapag = formapag;
            _serviçoid = serviçoid;
            _valorTotal = valortotal;
        }

        public Orçamento BuscarOrçamento(int id)
        {
            if (id > 0)
                return new DAL.OrçamentoDAL().Obter(id);
            else
                return null;
        }
    }
}
