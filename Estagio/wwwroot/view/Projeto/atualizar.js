var atualizar = {
    init: function () {
        atualizar.carregaDados();
    },
    getId: function () {
        var param = window.location.search.substring(1).split('&');
        var id = param[0].split('=');
        return id[1];
    },
    carregaDados: function () {
        var id = atualizar.getId();
        if (id != null) {
            var config = {
                method: "GET",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                }
            }
            fetch("/Projeto/BuscarProjeto?id=" + id, config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    if (dadosObj != null)
                        atualizar.carregaServiço(dadosObj);
                })
                .catch(function () {
                });
        }
    },
    carregaServiço: function (dadosObj) {

        document.getElementById("status").value = dadosObj.status;

        document.getElementById("btnAtualizar").value = "Atualizar";
    },

    btnAtualizar: function () {
        var valida = atualizar.validaDados();
        var id = atualizar.getId();
        if (btnAtualizar.value == "Atualizar") {
            var config = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                credentials: 'include', //inclui cookies
                body: JSON.stringify(valida.dados)
            };
            fetch("/Projeto/Att?id=" + id, config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    alert("Projeto Atualizado com sucesso");
                    window.location.href = "/Projeto/pesquisa";
                })
                .catch(function () {
                    document.getElementById("divMsg").innerHTML = "deu erro";
                })

        }
    },

    validaDados: function () {
        var Status = document.getElementById("status").value;

        var erro = "";
        if (Status.trim() == "") {

            erro += "Status Não informado.<br>";
        }
        valida = {
            msg: erro,
            dados: {
                Status
            }
        }
        return valida;

    },

}

atualizar.init();
