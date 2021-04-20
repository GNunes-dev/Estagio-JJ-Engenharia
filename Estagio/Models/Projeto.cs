using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estagio.Models
{
    public class Projeto
    {
        private int _id;
        private Cliente _cliente;
        private Setor _setor;
        private Funcionario _funcionario;
        private string _formapag;
        private string _descriçao;
        private string _dtinicial;
        private string _dtprevfinal;
        private string _dtFinal;
        private string _endereco;
        private string _bairro;
        private string _cep;
        private Cidade _cidade;
        private Estado _estado;
        private double _valorTotal;
        private string _status;

        public int Id { get => _id; set => _id = value; }
        public string Descriçao { get => _descriçao; set => _descriçao = value; }
        public string dtInicial { get => _dtinicial; set => _dtinicial = value; }
        public Cliente Cliente { get => _cliente; set => _cliente = value; }
        public Setor Setor { get => _setor; set => _setor = value; }
        public Funcionario Funcionario { get => _funcionario; set => _funcionario = value; }
        public string FormaPag { get => _formapag; set => _formapag = value; }
        public string dtPrevFinal { get => _dtprevfinal; set => _dtprevfinal = value; }
        public string dtFinal { get => _dtFinal; set => _dtFinal = value; }
        public string Endereco { get => _endereco; set => _endereco = value; }
        public string Bairro { get => _bairro; set => _bairro = value; }
        public string Cep { get => _cep; set => _cep = value; }
        public Cidade Cidade { get => _cidade; set => _cidade = value; }
        public Estado Estado { get => _estado; set => _estado = value; }
        public double valorTotal { get => _valorTotal; set => _valorTotal = value; }
        public string Status { get => _status; set => _status = value; }

        public Projeto()
        {
            this._id = 0;
            this._descriçao = "";
            this._dtinicial = "";
            this._cliente = null;
            this._setor = null;
            this._funcionario = null;
            this._formapag = "";
            this._dtprevfinal = "";
            this._dtFinal = "";
            this._endereco = "";
            this._bairro = "";
            this._cep = "";
            this._cidade = null;
            this._estado = null;
            this._valorTotal = 0;
            this._status = "";
        }

        public Projeto(int id, Cliente cliente, Setor setor, Funcionario funcionario, string formapag, string descriçao, string dtinicial, string dtprevfinal, string dtFinal, string endereco, string bairro, string cep, Cidade cidade, Estado estado, double valorTotal, string status)
        {
            _id = id;
            _cliente = cliente;
            _setor = setor;
            _funcionario = funcionario;
            _formapag = formapag;
            _descriçao = descriçao;
            _dtinicial = dtinicial;
            _dtprevfinal = dtprevfinal;
            _dtFinal = dtFinal;
            _endereco = endereco;
            _bairro = bairro;
            _cep = cep;
            _cidade = cidade;
            _estado = estado;
            _valorTotal = valorTotal;
            _status = status;
        }
    }
}
