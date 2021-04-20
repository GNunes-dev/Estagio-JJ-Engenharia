using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estagio.Models
{
    public class ItensServ
    {
        private int _idserv;
        private int _idgerarserv;


        public int Idserv { get => _idserv; set => _idserv = value; }
        public int Idgerarserv { get => _idgerarserv; set => _idgerarserv = value; }

        public ItensServ()
        {
            this._idserv = 0;
            this._idgerarserv = 0;
        }

        public ItensServ(int idserv, int idgerarserv)
        {
            _idserv = idserv;
            _idgerarserv = idgerarserv;
        }
    }
}
