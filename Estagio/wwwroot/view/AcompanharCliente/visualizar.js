//var idcli = visualizar.retornaid();
var visualizar = {
    init: function () {
        visualizar.getId();
    },
    getId: function () {
        $.ajax({
            type: 'POST',
            url: '/HomeCli/pegaId',
            contentType: 'application/json',
            success: function (res) {
                if (res > 0) {
                    let id = res;
                    visualizar.carregaDados(res);
                }
                else {
                    var erro = "";
                    erro += "<div class=\"alert alert-danger\" role=\"alert\">";
                    erro += "erro ao pegar id";
                    erro += "</div>";
                    document.getElementById('divMsg').innerHTML = erro;
                }
            }
        });
    },
    retornaid: function () {
        $.ajax({
            type: 'POST',
            url: '/HomeCli/pegaId',
            contentType: 'application/json',
            success: function (res) {
                if (res > 0) {
                    return res;
                }
                else {
                    var erro = "";
                    erro += "<div class=\"alert alert-danger\" role=\"alert\">";
                    erro += "erro ao pegar id";
                    erro += "</div>";
                    document.getElementById('divMsg').innerHTML = erro;
                }
            }
        });
    },
    carregaDados: function (id) {
        if (id != null) {
            var config = {
                method: "GET",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                }
            }
            fetch("/Cliente/BuscarCliente?id=" + id, config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    if (dadosObj != null)
                        visualizar.carregaCliente(dadosObj);
                })
                .catch(function () {
                });
        }
    },
    carregaCliente: function (dadosObj) {
        document.getElementById("login").value = dadosObj.login;
        document.querySelector("#senha").value = dadosObj.senha;
        document.getElementById("nome").value = dadosObj.nome;
        document.getElementById("email").value = dadosObj.email;
        document.getElementById("cpf").value = dadosObj.cpf;
        document.getElementById("telefone").value = dadosObj.telefone;
        document.getElementById("rg").value = dadosObj.rg;
        document.getElementById("endereco").value = dadosObj.endereco;
        document.getElementById("bairro").value = dadosObj.bairro;
        document.getElementById("cep").value = dadosObj.cep;
        visualizar.buscarEstados(dadosObj.estado);
        document.getElementById("uf").value = dadosObj.estado;


        visualizar.buscarCidades(dadosObj.estado, dadosObj.cidade);
    },

    btnCadastrar: function () {
        var valida = visualizar.valida();

            var config = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                credentials: 'include', //inclui cookies
                body: JSON.stringify(valida.dados)
            };
            fetch("/Cliente/EditarCli", config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    alert("Alterado com sucesso");
                    window.location.href = "/AcompanharCliente/visualizar";
                })
                .catch(function () {
                    document.getElementById("divMsg").innerHTML = "deu erro";
                })
    },
    valida: function () {
        var Id = idcli;
        var Login = document.getElementById("login").value;
        var Senha = document.querySelector("#senha").value;
        var Nome = document.getElementById("nome").value;
        var Email = document.getElementById("email").value;
        var Cpf = document.getElementById("cpf").value;
        var Telefone = document.getElementById("telefone").value;
        var Rg = document.getElementById("rg").value;
        var Endereco = document.getElementById("endereco").value;
        var Bairro = document.getElementById("bairro").value;
        var Cep = document.getElementById("cep").value;
        var Cidade = document.getElementById("selCidade").value;
        var Estado = document.getElementById("uf").value;
        var erro = "";

        if (Nome.trim() == "") {

            erro += "Nome Não informado.<br>";
        }
        if (Email.trim() == "") {

            erro += "Email Não informado.<br>";
        }
        if (Cpf.trim() == "") {

            erro += "Cpf Não informado.<br>";
        }
        if (Telefone.trim() == "") {

            erro += "Telefone Não informado.<br>";
        }
        if (Rg.trim() == "") {

            erro += "RG Não informado.<br>";
        }
        if (Endereco.trim() == "") {

            erro += "Endereco Não informado.<br>";
        }
        if (Bairro.trim() == "") {

            erro += "Bairro Não informado.<br>";
        }
        if (Cep.trim() == "") {

            erro += "Cep Não informado.<br>";
        }
        if (Estado.trim() == "") {

            erro += "Estado Não informado.<br>";
        }
        if (Cidade.trim() == "") {

            erro += "Cidade Não informada.<br>";
        }
        valida = {
            msg: erro,
            dados: {
                Id,
                Login,
                Senha,
                Nome,
                Email,
                Cpf,
                Telefone,
                Rg,
                Endereco,
                Bairro,
                Cep,
                Cidade,
                Estado
            }
        }
        return valida;

    },

    buscarEstados: function (id) {

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
                document.getElementById("uf").value = id;
            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })

    },

    buscarCidades: function (uf, cidade) {


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
                document.getElementById("selCidade").value = cidade;
            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })
    }

}

visualizar.init();
