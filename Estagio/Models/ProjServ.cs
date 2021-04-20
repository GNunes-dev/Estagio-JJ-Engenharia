using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estagio.Models
{
    public class ProjServ
    {
        private int _idproj;
        private int _idserv;


        public int Id_serv { get => _idserv; set => _idserv = value; }
        public int Id_proj { get => _idproj; set => _idproj = value; }

        public ProjServ()
        {
            this._idserv = 0;
            this._idproj = 0;
        }

        public ProjServ(int idserv, int idproj)
        {
            _idserv = idserv;
            _idproj = _idproj;
        }
    }
}
