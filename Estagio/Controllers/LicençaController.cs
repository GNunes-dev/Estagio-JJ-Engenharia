using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace Estagio.Controllers
{
    public class LicençaController : Controller
    {
        [Authorize("CookieAuth")]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;
            string msg = "";

            Models.Licença licença = new Models.Licença();
            licença.clienteId = new Models.Cliente();
            licença.Funcionario = new Models.Funcionario();

            licença.Nome = (dados["Nome"]);
            licença.dtVencimento = (dados["dtVencimento"]);
            licença.numLicença = (dados["numLicença"]);
            licença.numProcesso = (dados["numProcesso"]);
            licença.dtInicial = (dados["dtInicial"]);
            licença.Cnae = (dados["Cnae"]);
            licença.Funcionario.Id = Convert.ToInt32((dados["Crea"]));
            licença.Versao = (dados["Versao"]);
            licença.clienteId.Id = Convert.ToInt32((dados["Cliente"]));
            licença.orgaoId = Convert.ToInt32((dados["Orgao"]));
            licença.setorId = Convert.ToInt32((dados["Setor"]));
            licença.valorTotal = Convert.ToDouble((dados["valorTotal"]));

            CamadaNegocio.LicençaCN
                    lcn = new CamadaNegocio.LicençaCN();
            (operacao, msg) = lcn.Criar(licença);

            int id = licença.Id;
            return Json(new
            {
                id,
                operacao,
                msg
            });

        }

        public IActionResult Pesquisa()
        {
            return View();
        }

        public IActionResult Atualizar()
        {
            return View();
        }

        public IActionResult Visualizar()
        {
            return View();
        }

        public IActionResult Pesquisar(string nome)
        {
            CamadaNegocio.LicençaCN lcn = new CamadaNegocio.LicençaCN();

            List<Models.Licença> licença = lcn.Pesquisar(nome);

            return Json(licença);
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            CamadaNegocio.LicençaCN scn = new CamadaNegocio.LicençaCN();
            bool operacao = scn.Excluir(id);

            return Json(new
            {
                operacao
            });
        }

        public IActionResult BuscarLicença(int Id)
        {
            object Dado = new object();
            CamadaNegocio.LicençaCN lcn = new CamadaNegocio.LicençaCN();

            var L = lcn.BuscarLicença(Id);
            Dado = (new
            {
                Id = L.Id,
                Nome = L.Nome,
                dtVencimento = L.dtVencimento,
                numLicença = L.numLicença,
                numProcesso = L.numProcesso,
                dtInicial = L.dtInicial,
                Cnae = L.Cnae,
                Crea = L.Funcionario,
                Versao = L.Versao,
                clienteId = L.clienteId,
                orgaoId = L.orgaoId,
                setorId = L.setorId,
                valorTotal = L.valorTotal
            });

            return Json(Dado);
        }

        public IActionResult ObterValor(int licença)
        {
            object Dado = new object();
            CamadaNegocio.LicençaCN scn = new CamadaNegocio.LicençaCN();

            var L = scn.BuscarLicença(licença);
            if (L == null)
            {
                Dado = null;
            }
            else
            {
                    Dado = (new
                    {
                        Id = L.Id,
                        Nome = L.Nome,
                        dtVencimento = L.dtVencimento,
                        numLicença = L.numLicença,
                        numProcesso = L.numProcesso,
                        dtInicial = L.dtInicial,
                        Cnae = L.Cnae,
                        Crea = L.Funcionario.Id,
                        Versao = L.Versao,
                        clienteId = L.clienteId,
                        orgaoId = L.orgaoId,
                        setorId = L.setorId,
                        valorTotal = L.valorTotal
                    });
            }

            return Json(Dado);
        }

        [HttpPost]
        public IActionResult Att(int id, [FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;
            string versao = dados["Versao"];
            string dtVenc = dados["dtVencimento"];
            CamadaNegocio.LicençaCN
                    fcn = new CamadaNegocio.LicençaCN();
            operacao = fcn.Att(id, versao, dtVenc);


            return Json(new
            {
                operacao
            });

        }
        public IActionResult BuscarLicençaCli(int id)
        {
            Estagio.CamadaNegocio.LicençaCN scn =
               new CamadaNegocio.LicençaCN();

            return Json(scn.BuscarLicençaCli(id));
        }
        public IActionResult ObterTodos()
        {
            Estagio.CamadaNegocio.LicençaCN scn =
               new CamadaNegocio.LicençaCN();

            return Json(scn.ObterTodos());
        }

        public JsonResult GetArquivo(int id)
        {
            object Dado = new object();
            DAL.LicençaDAL scn = new DAL.LicençaDAL();

            var L = scn.GetArquivo(id);
            if (L == null)
            {
                Dado = null;
            }
            else
            {
                Dado = (new
                {
                    Id = L.Id,
                    IdLic = L.IdLic,
                    Nome = L.Nome,
                    Formato = L.Formato,
                    Type = L.Type,
                    Tamanho = L.Tamanho,
                    Arquivo = L.Arq
                });
            }

            return Json(Dado);
        }

        public JsonResult AtualizarDoc()
        {
            int idlic = Convert.ToInt32(Request.Form["id"]);

            Models.Licença_Documento LicD = null;
            if (Request.Form.Files.Count > 0)
            {
                MemoryStream ms = new MemoryStream();
                Request.Form.Files[0].CopyTo(ms);
                byte[] arq = ms.ToArray();
                string nome = Request.Form.Files[0].FileName;
                string formato = System.IO.Path.GetExtension(nome);
                string type = Request.Form.Files[0].ContentType;
                string desc = Request.Form.Files[0].Name;
                int tamanho = Convert.ToInt32(Request.Form.Files[0].Length);
                LicD = new Models.Licença_Documento(idlic, nome, formato, type, tamanho, arq);
            }

            DAL.LicençaDAL ld = new DAL.LicençaDAL();
            if (ld.AtualizarDoc(LicD))
                return Json(new
                {
                    ok = true,
                    msg = "Documento Atualizado com sucesso!"
                });
            else
                return Json(new
                {
                    ok = false,
                    msg = "Erro ao atualizar documento!"
                });
        }

        public IActionResult ViewDoc(int id)
        {
            CamadaNegocio.LicençaCN bl = new CamadaNegocio.LicençaCN();
            Models.Licença_Documento arquivo = new Models.Licença_Documento(); 
            arquivo = bl.BuscarArquivo(id);
            if (arquivo == null || arquivo.Arq == null)
            {
                return Json("Arquivo invalido!");
            }
            else
            {
                return File(arquivo.Arq, arquivo.Type);
            }
        }

    }
}