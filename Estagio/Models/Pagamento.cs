using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estagio.Models
{
    public class Pagamento
    {

        private int _id;
        private string _descriçao;
        private string _dtvencimento;
        private double _valor;
        private string _formapag;
        private bool _quitado;
        private string _dtpagamento;
        private double _valorparcial;

        public int Id { get => _id; set => _id = value; }
        public string Descriçao { get => _descriçao; set => _descriçao = value; }
        public string dtVencimento { get => _dtvencimento; set => _dtvencimento = value; }
        public double Valor { get => _valor; set => _valor = value; }
        public string FormaPag { get => _formapag; set => _formapag = value; }
        public bool Quitado { get => _quitado; set => _quitado = value; }
        public string dtPagamento { get => _dtpagamento; set => _dtpagamento = value; }
        public double ValorParcial { get => _valorparcial; set => _valorparcial = value; }
        public Pagamento()
        {
            this._id = 0;
            this._descriçao = "";
            this._dtvencimento = "";
            this._valor = 0;
            this._formapag = null;
            this._quitado = false;
            this._dtpagamento = "";
            this._valorparcial = 0;
        }


        public Pagamento(int id, string descriçao, string dtvencimento, double valor, string formapag, bool quitado, string dtpagamento, double valorparcial)
        {
            _id = id;
            _descriçao = descriçao;
            _dtvencimento = dtvencimento;
            _valor = valor;
            _formapag = formapag;
            _quitado = quitado;
            _dtpagamento = dtpagamento;
            _valorparcial = valorparcial;
        }
    }
}
