namespace Estagio.Models
{
    public class TipoDespesa
    {
        private int _id;
        private string _descriçao;


        public int Id { get => _id; set => _id = value; }
        public string Descriçao { get => _descriçao; set => _descriçao = value; }

        public TipoDespesa()
        {
            this._id = 0;
            this._descriçao = "";
        }

        public TipoDespesa(int id, string descriçao)
        {
            _id = id;
            _descriçao = descriçao;
        }

        public TipoDespesa BuscarTipoDespesa(int id)
        {
            if (id > 0)
                return new DAL.TipoDespesaDAL().Obter(id);
            else
                return null;
        }
    }
}
