namespace Estagio.Models
{
    public class OrgaoLicenciamento
    {
        private int _id;
        private string _descriçao;
        private double _valor;


        public int Id { get => _id; set => _id = value; }
        public string Descriçao { get => _descriçao; set => _descriçao = value; }
        public double Valor { get => _valor; set => _valor = value; }

        public OrgaoLicenciamento()
        {
            this._id = 0;
            this._descriçao = "";
            this._valor = 0;
        }

        public OrgaoLicenciamento(int id, string descriçao, double valor)
        {
            _id = id;
            _descriçao = descriçao;
            _valor = valor;
        }

        public OrgaoLicenciamento BuscarOrgao(int id)
        {
            if (id > 0)
                return new DAL.OrgaoLicenciamentoDAL().Obter(id);
            else
                return null;
        }
    }
}
