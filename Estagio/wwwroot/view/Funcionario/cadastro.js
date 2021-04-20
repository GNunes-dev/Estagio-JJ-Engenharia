var cadastro = {
    init: function () {
        cadastro.buscarEstados();
        cadastro.carregaDados();
    },
    getId: function () {
        var param = window.location.search.substring(1).split('&');
        var id = param[0].split('=');
        return id[1];
    },
    carregaDados: function () {
        var id = cadastro.getId();
        if (id != null) {
            var config = {
                method: "GET",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                }
            }
            fetch("/Funcionario/BuscarFuncionario?id=" + id, config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {

                    cadastro.carregaFuncionario(dadosObj);
                })
                .catch(function () {
                });
        }},
    carregaFuncionario: function (dadosObj) {
        document.getElementById("login").value = dadosObj.login;
        document.querySelector("#senha").value = dadosObj.senha;
        document.getElementById("nome").value = dadosObj.nome;
        document.getElementById("email").value = dadosObj.email;
        document.getElementById("cpf").value = dadosObj.cpf;
        document.getElementById("telefone").value = dadosObj.telefone;
        document.getElementById("rg").value = dadosObj.rg;
        document.getElementById("crea").value = dadosObj.crea;
        document.getElementById("endereco").value = dadosObj.endereco;
        document.getElementById("bairro").value = dadosObj.bairro;
        document.getElementById("cep").value = dadosObj.cep;

        cadastro.buscarEstados(dadosObj.estado);
        document.getElementById("uf").value = dadosObj.estado;


        cadastro.buscarCidades(dadosObj.estado, dadosObj.cidade);
        
        document.getElementById("btnCadastrar").value = "Alterar";
    },

    btnCadastrar: function () {
        var valida = cadastro.validaDados();
        if (btnCadastrar.value == "Alterar") {
            var config = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                credentials: 'include', //inclui cookies
                body: JSON.stringify(valida.dados)
            };
            fetch("/Funcionario/Editar", config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    alert("Funcionario Alterado com sucesso");
                    window.location.href = "/Funcionario/pesquisa";
                })
                .catch(function () {
                    document.getElementById("divMsg").innerHTML = "deu erro";
                })
        }
        else {
            if (valida.msg == "") {

                var config = {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json; charset=utf-8"
                    },
                    credentials: 'include', //inclui cookies
                    body: JSON.stringify(valida.dados)  //serializa
                };

                fetch("/Funcionario/Criar", config)
                    .then(function (dadosJson) {
                        var obj = dadosJson.json(); //deserializando
                        return obj;
                    })
                    .then(function (dadosObj) {
                        if (dadosObj.msg == "") {
                            alert("Funcionario cadastrado com sucesso");
                            window.location.reload(true);
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
        }
    },
    validaDados: function () {
        var Id = cadastro.getId();
        var Login = document.getElementById("login").value;
        var Senha = document.querySelector("#senha").value;
        var Nome = document.getElementById("nome").value;
        var Email = document.getElementById("email").value;
        var Cpf = document.getElementById("cpf").value;
        var Telefone = document.getElementById("telefone").value;
        var Rg = document.getElementById("rg").value;
        var Crea = document.getElementById("crea").value;
        var Endereco = document.getElementById("endereco").value;
        var Bairro = document.getElementById("bairro").value;
        var Cep = document.getElementById("cep").value;
        var Cidade = document.getElementById("selCidade").value;
        var Estado = document.getElementById("uf").value;
        var erro = "";
        if (Login.trim() == "") {

            erro += "Informe o login.<br>";
        }
        if (Senha.trim() == "") {

            erro += "Senha não informada.<br>";
        }
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
        if (Crea.trim() == "") {

            erro += "Crea Não informado.<br>";
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
        if (Cidade.trim() == "") {

            erro += "Cidade Não informado.<br>";
        }
        if (Estado.trim() == "") {

            erro += "Estado Não informado.<br>";
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
                Crea,
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
cadastro.init();
