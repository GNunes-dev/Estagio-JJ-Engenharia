using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estagio.Models
{
    public class ProjLic
    {
        private int _idproj;
        private int _idlic;


        public int Id_lic { get => _idlic; set => _idlic = value; }
        public int Id_proj { get => _idproj; set => _idproj = value; }

        public ProjLic()
        {
            this._idlic = 0;
            this._idproj = 0;
        }

        public ProjLic(int idlic, int idproj)
        {
            _idlic = idlic;
            _idproj = _idproj;
        }
    }
}
