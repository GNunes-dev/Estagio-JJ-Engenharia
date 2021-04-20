namespace Estagio.Models
{
    public class Serviço
    {
        private int _id;
        private string _nome;
        private string _descriçao;
        private double _valor;
        private Setor _idsetor;

        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Descriçao { get => _descriçao; set => _descriçao = value; }
        public double Valor { get => _valor; set => _valor = value; }
        public Setor IdSetor { get => _idsetor; set => _idsetor = value; }

        public Serviço()
        {
            this._id = 0;
            this._nome = "";
            this._descriçao = "";
            this._valor = 0;
            this._idsetor = null;
        }


        public Serviço(int id, string nome, string descriçao, double valor, Setor idsetor)
        {
            _id = id;
            _nome = nome;
            _descriçao = descriçao;
            _valor = valor;
            _idsetor = idsetor;
        }

        public Serviço BuscarServiço(int id)
        {
            if (id > 0)
                return new DAL.ServiçoDAL().Obter(id);
            else
                return null;
        }
    }
}
