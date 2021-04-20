using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Estagio.Controllers
{
    public class FormaPagamento : Controller
    {
        public List<string> ObterTodos()
        {
            List<string> formapag = new List<string>();

            formapag.Add("Dinheiro");
            formapag.Add("Cartão");

            return formapag;
        }

        public List<int> NumeroParcelas()
        {
            List<int> numparc = new List<int>();

            numparc.Add(1);
            numparc.Add(2);
            numparc.Add(3);
            numparc.Add(4);
            numparc.Add(5);
            numparc.Add(6);

            return numparc;
        }
    }
}
