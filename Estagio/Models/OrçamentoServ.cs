using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estagio.Models
{
    public class OrçamentoServ
    {
        private int _idserv;
        private int _idor;


        public int Idserv { get => _idserv; set => _idserv = value; }
        public int Idor { get => _idor; set => _idor = value; }

        public OrçamentoServ()
        {
            this._idserv = 0;
            this._idor = 0;
        }

        public OrçamentoServ(int idserv, int idor)
        {
            _idserv = idserv;
            _idor = idor;
        }
    }
}
