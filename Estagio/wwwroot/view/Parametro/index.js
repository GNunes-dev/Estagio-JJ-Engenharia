var index =
{

    btnCadastrar: function () {
        var valida = index.valida();
        if (valida.msg == "") {

            var config = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                credentials: 'include', //inclui cookies
                body: JSON.stringify(valida.dados)  //serializa
            };

            fetch("/Parametro/Criar", config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    if (dadosObj.msg == "") {
                        alert("Parametro cadastrado com sucesso");
                        window.location.href = "/Funcionario/Cadastro";
                    }
                    else {
                        var erro = "";
                        erro += "<div class=\"alert alert-danger\" role=\"alert\">";
                        erro += dadosObj.msg;
                        erro += "</div>";
                        document.getElementById('divMsg').innerHTML = erro;
                    }
                })
                .catch(function () {
                    document.getElementById("divMsg").innerHTML = "deu erro";
                })

        } else {
            var erro = "";
            erro += "<div class=\"alert alert-danger\" role=\"alert\">";
            erro += valida.msg;
            erro += "</div>";
            document.getElementById('divMsg').innerHTML = erro;
        }
    },
    valida: function () {
        var Razao = document.getElementById("razao").value;
        var NomeFantasia = document.getElementById("nomefantasia").value;
        var CNPJ = document.getElementById("cnpj").value;
        var Email = document.getElementById("email").value;
        var IE = document.getElementById("ie").value;
        var DataInicio = document.getElementById("datainicio").value;
        var Endereco = document.getElementById("endereco").value;
        var Bairro = document.getElementById("bairro").value;
        var Telefone = document.getElementById("telefone").value;
        var Cep = document.getElementById("cep").value;
        var Cidade = document.getElementById("selCidade").value;
        var Estado = document.getElementById("uf").value;
        var erro = "";
        if (Razao.trim() == "") {

            erro += "Informe a Razao.<br>";
        }
        if (NomeFantasia.trim() == "") {

            erro += "Nome não informado.<br>";
        }
        if (CNPJ.trim() == "") {

            erro += "CNPJ Não informado.<br>";
        }
        if (Email.trim() == "") {

            erro += "Email Não informado.<br>";
        }
        if (IE.trim() == "") {

            erro += "IE Não informado.<br>";
        }
        if (DataInicio.trim() == "") {

            erro += "Data Inicio Não informado.<br>";
        }
        if (Endereco.trim() == "") {

            erro += "Endereco Não informado.<br>";
        }
        if (Bairro.trim() == "") {

            erro += "Bairro Não informado.<br>";
        }
        if (Telefone.trim() == "") {

            erro += "Telefone Não informado.<br>";
        }
        if (Cep.trim() == "") {

            erro += "Cep Não informado.<br>";
        }
        if (Cidade.trim() == "") {

            erro += "Cidade Não informado.<br>";
        }
        if (Estado.trim() == "") {

            erro += "Estado Não informado.<br>";
        }
        valida = {
            msg: erro,
            dados: {
                Razao,
                NomeFantasia,
                CNPJ,
                Email,
                IE,
                DataInicio,
                Endereco,
                Bairro,
                Telefone,
                Cep,
                Cidade,
                Estado
            }
        }
        return valida;

    },

    buscarEstados: function () {

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Cidade/ObterEstados", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var selUF = document.getElementById("uf");
                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].nome}</option>`;
                }

                selUF.innerHTML = opts;

            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })

    },

    buscarCidades: function (uf) {


        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui 
        };

        fetch("/Cidade/ObterCidades?uf=" + uf, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var selCidade = document.getElementById("selCidade");
                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].nome}</option>`;
                }

                selCidade.innerHTML = opts;

            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })
    }
}
index.buscarEstados();


