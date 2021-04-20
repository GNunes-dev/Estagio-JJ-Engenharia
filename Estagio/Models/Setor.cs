namespace Estagio.Models
{
    public class Setor
    {
        private int _id;
        private string _descriçao;


        public int Id { get => _id; set => _id = value; }
        public string Descriçao { get => _descriçao; set => _descriçao = value; }

        public Setor()
        {
            this._id = 0;
            this._descriçao = "";
        }

        public Setor(int id, string descriçao)
        {
            _id = id;
            _descriçao = descriçao;
        }

        public Setor BuscarSetor(int id)
        {
            if (id > 0)
                return new DAL.SetorDAL().Obter(id);
            else
                return null;
        }
    }
}
