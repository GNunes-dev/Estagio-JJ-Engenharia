var cadastro = {
    init: function () {
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
            fetch("/OrgaoLicenciamento/BuscarOrgao?id=" + id, config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {

                    cadastro.carregaOrgao(dadosObj);
                })
                .catch(function () {
                });
        }
    },
    carregaOrgao: function (dadosObj) {
        document.getElementById("descricao").value = dadosObj.descriçao;
        document.getElementById("valor").value = dadosObj.valor;
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
            fetch("/OrgaoLicenciamento/Editar", config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    alert("Orgao Alterado com sucesso");
                    window.location.href = "/OrgaoLicenciamento/pesquisa";
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

                fetch("/OrgaoLicenciamento/Criar", config)
                    .then(function (dadosJson) {
                        var obj = dadosJson.json(); //deserializando
                        return obj;
                    })
                    .then(function (dadosObj) {
                        if (dadosObj.msg == "") {
                            alert("Orgão cadastrado com sucesso");
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
        var Descriçao = document.getElementById("descricao").value;
        var Valor = document.getElementById("valor").value;
        var erro = "";        
         if (Descriçao.trim() == "") {

             erro += "Descrição Não informada.<br>";
        }
         if (Valor.trim() == "") {

             erro += "Valor Não informado.<br>";
        }

        valida = {
            msg: erro,
            dados: {
                Id,
                Descriçao,
                Valor
            }
        }
        return valida;

    }
}
cadastro.init();