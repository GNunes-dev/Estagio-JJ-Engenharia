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
            fetch("/Projeto/BuscarProjeto?id=" + id, config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    if (dadosObj != null)
                        visualizar.carregaServiço(dadosObj);
                })
                .catch(function () {
                });
        }
    },
    carregaServiço: function (dadosObj) {

        document.getElementById("cliente").value = dadosObj.cliente.nome;
        document.getElementById("setor").value = dadosObj.setor.descriçao;
        document.getElementById("funcionario").value = dadosObj.funcionario.crea;
        document.getElementById("formapag").value = dadosObj.formaPag;
        document.getElementById("descriçao").value = dadosObj.descriçao;
        var dateprev = dadosObj.dtPrevFinal.split(' ');
        var date = dadosObj.dtInicial.split(' ');
        document.getElementById("dtinicial").value = date[0];
        document.getElementById("dtprevfinal").value = dateprev[0];
        document.getElementById("endereco").value = dadosObj.endereco;
        document.getElementById("bairro").value = dadosObj.bairro;
        document.getElementById("cep").value = dadosObj.cep;
        document.getElementById("status").value = dadosObj.status;
        document.getElementById("valortotal").value = dadosObj.valorTotal;

        visualizar.buscarEstado(dadosObj.estado.id);
        visualizar.buscarCidade(dadosObj.cidade.id);

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

        fetch("/Projeto/BuscarProjetoServiço?id=" + id, config)
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

        fetch("/Projeto/BuscarProjetoLicença?id=" + id, config)
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
    },

    buscarEstado: function (id) {
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Cidade/GetEstado?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {
                document.getElementById("uf").value = dadosObj.nome;
            })
            .catch(function () {

            })
    },

    buscarCidade: function (id) {
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Cidade/GetCidade?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {
                document.getElementById("selCidade").value = dadosObj.nome;
            })
            .catch(function () {

            })
    }
}

visualizar.init();
