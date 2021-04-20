namespace Estagio.Models
{
    public class Cidade
    {
        int _id;
        string _nome;
        int _estadoid;

        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public int EstadoId { get => _estadoid; set => _estadoid = value; }
        public Cidade()
        {
            _id = 0;
            _nome = "";
            _estadoid = 0;
        }
    }
}
