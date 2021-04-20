var visualizar = {
    init: function () {
        visualizar.carregaDados();
    },
    getId: function () {
        var param = window.location.search.substring(1).split('&');
        var id = param[0].split('=');
        return id[1];
    },
    carregaDados: function () {
        var id = visualizar.getId();
        if (id != null) {
            var config = {
                method: "GET",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                }
            }
            fetch("/Licença/BuscarLicença?id=" + id, config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    if (dadosObj != null)
                        visualizar.carregaLicença(dadosObj);
                })
                .catch(function () {
                });
        }
    },
    carregaLicença: function (dadosObj) {

        document.getElementById("cliente").value = dadosObj.clienteId.nome;
        document.getElementById("crea").value = dadosObj.crea.crea;
        visualizar.BuscarSetor(dadosObj.setorId);

        var dtvenc = dadosObj.dtVencimento.split(' ');
        var date = dadosObj.dtInicial.split(' ');
        document.getElementById("dtinicial").value = date[0];
        document.getElementById("vencimento").value = dtvenc[0];
        visualizar.BuscarOrgao(dadosObj.orgaoId);
        document.getElementById("valortotal").value = visualizar.dinheiro(dadosObj.valorTotal);
        document.getElementById("nome").value = dadosObj.nome;
        document.getElementById("numprocesso").value = dadosObj.numProcesso;
        document.getElementById("numlicença").value = dadosObj.numLicença;
        document.getElementById("versao").value = dadosObj.versao;
        document.getElementById("cnae").value = dadosObj.cnae;
    },

    dinheiro: function (valor) {
        if (valor != null && valor != 0) {
            valor = valor + "".replace(',', '.');
            valor = parseFloat(valor);
            valor = valor.toFixed(2);
            valor = valor.replace(/[\D]+/g, '');
            valor = valor.replace(/([0-9]{2})$/g, ",$1");
            return 'R$ ' + valor
        } else
            return "";

    },

    BuscarSetor: function (id) {
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Setor/BuscarSetor?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {
                document.getElementById("setor").value = dadosObj.descriçao;
            })
            .catch(function () {

            })
    },

    BuscarOrgao: function (id) {
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/OrgaoLicenciamento/BuscarOrgao?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {
                document.getElementById("orgao").value = dadosObj.descriçao;
            })
            .catch(function () {

            })
    }
}

visualizar.init();
