using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estagio.Models
{
    public class OrçamentoLic
    {
        private int _idlic;
        private int _idor;


        public int Id_lic { get => _idlic; set => _idlic = value; }
        public int Id_or { get => _idor; set => _idor = value; }

        public OrçamentoLic()
        {
            this._idlic = 0;
            this._idor = 0;
        }

        public OrçamentoLic(int idlic, int idor)
        {
            _idlic = idlic;
            _idor = idor;
        }
    }
}
