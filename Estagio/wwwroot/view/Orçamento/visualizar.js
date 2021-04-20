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
            fetch("/Orçamento/BuscarOrçamento?id=" + id, config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    if (dadosObj != null)
                        visualizar.carregaOrçamento(dadosObj);
                })
                .catch(function () {
                });
        }
    },
    carregaOrçamento: function (dadosObj) {
        document.getElementById("descriçao").value = dadosObj.descriçao;
        document.getElementById("cliente").value = dadosObj.clienteId.nome;
        document.getElementById("setor").value = dadosObj.setorId.descriçao;
        document.getElementById("formapag").value = dadosObj.formaPag;

        var data = dadosObj.dtVencimento.split(' ');
        var date2 = data[0].split('/');
        var thedate = date2[2] + "-" + date2[1] + "-" + date2[0];
        document.getElementById("vencimento").value = thedate;
        document.getElementById("valortotal").value = dadosObj.valorTotal;

        visualizar.BuscarListaServiço(dadosObj.id);
        visualizar.BuscarListaLicença(dadosObj.id);

    },

    BuscarListaServiço: function (id) {
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Orçamento/BuscarItensServiço?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var linhas = "";
                for (var i = 0; i < dadosObj.length; i++) {
                    var template =
                        `<tr>
                            <td>${dadosObj[i].nome}</td>
                            <td>${dadosObj[i].valor}</td>
                      </tr>`

                    linhas += template;
                }
                if (linhas == "") {

                    linhas = `<tr><td colspan="3">Sem resultado.</td></tr>`
                }
                tbodyServiços.innerHTML = linhas;
            })
            .catch(function () {
                tbodyServiços.innerHTML = `<tr><td colspan="3">Deu erro...</td></tr>`
            })
    },

    BuscarListaLicença: function (id) {
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Orçamento/BuscarItensLicença?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var linhas = "";
                for (var i = 0; i < dadosObj.length; i++) {
                    var template =
                        `<tr>
                            <td>${dadosObj[i].nome}</td>
                            <td>${dadosObj[i].valorTotal}</td>
                      </tr>`

                    linhas += template;
                }
                if (linhas == "") {

                    linhas = `<tr><td colspan="3">Sem resultado.</td></tr>`
                }
                tbodyLicenças.innerHTML = linhas;
            })
            .catch(function () {
                tbodyLicenças.innerHTML = `<tr><td colspan="3">Deu erro...</td></tr>`
            })
    }
}

visualizar.init();
