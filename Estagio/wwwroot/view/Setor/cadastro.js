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
            fetch("/Setor/BuscarSetor?id=" + id, config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {

                    cadastro.carregaSetor(dadosObj);
                })
                .catch(function () {
                });
    }
    },
    carregaSetor: function (dadosObj) {
        document.getElementById("descriçao").value = dadosObj.descriçao;
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
            fetch("/Setor/Editar", config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    alert("Setor Alterado com sucesso");
                    window.location.href = "/Setor/pesquisa";
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

                fetch("/Setor/Criar", config)
                    .then(function (dadosJson) {
                        var obj = dadosJson.json(); //deserializando
                        return obj;
                    })
                    .then(function (dadosObj) {
                        if (dadosObj.msg == "") {
                            alert("Setor cadastrado com sucesso");
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
        var Descriçao = document.getElementById("descriçao").value;
        var erro = "";        
         if (Descriçao.trim() == "") {

             erro += "Descrição Não informada.<br>";
        }

        valida = {
            msg: erro,
            dados: {
                Id,
                Descriçao
            }
        }
        return valida;

    }
}
cadastro.init();