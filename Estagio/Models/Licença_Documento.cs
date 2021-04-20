using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estagio.Models
{
    public class Licença_Documento
    {

        private int _id;
        private int _idlic;
        private string _nome;
        private string _formato;
        private string _type;
        private double _tamanho;
        private byte[] _arq;

        public int Id { get => _id; set => _id = value; }
        public int IdLic { get => _idlic; set => _idlic = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Formato { get => _formato; set => _formato = value; }
        public string Type { get => _type; set => _type = value; }
        public double Tamanho { get => _tamanho; set => _tamanho = value; }
        public byte[] Arq { get => _arq; set => _arq = value; }

        public Licença_Documento()
        {
            this._id = 0;
            this._idlic = 0;
            this._nome = "";
            this._formato = "";
            this._type = "";
            this._tamanho = 0;
            this._arq = null;
        }

        public Licença_Documento(int id, int idlic, string nome, string formato, string type, double tamanho, byte[] arq)
        {
            _id = id;
            this._idlic = idlic;
            this._nome = nome;
            this._formato = formato;
            this._type = type;
            this._tamanho = tamanho;
            this._arq = arq;
        }

        public Licença_Documento( int idlic, string nome, string formato, string type, double tamanho, byte[] arq)
        {
            this._idlic = idlic;
            this._nome = nome;
            this._formato = formato;
            this._type = type;
            this._tamanho = tamanho;
            this._arq = arq;
        }

    }
}
