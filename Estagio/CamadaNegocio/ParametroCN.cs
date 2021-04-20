namespace Estagio.CamadaNegocio
{
    public class ParametroCN
    {
        public (bool, string) Criar(Models.Parametro parametro)
        {
            string msg = "";
            bool operacao = false;
            DAL.ParametroDAL pbd = new DAL.ParametroDAL();

            operacao = pbd.Criar(parametro);

            return (operacao, msg);
        }
    }
}
