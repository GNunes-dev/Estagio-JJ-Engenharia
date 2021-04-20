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
            fetch("/GerarServiço/BuscarServiço?id=" + id, config)
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

        document.getElementById("cliente").value = dadosObj.clienteId.nome;
        visualizar.BuscarFunc(dadosObj.funcionarioId);
        visualizar.BuscarSetor(dadosObj.setorId);

        var dateprev = dadosObj.dtPrevFinal.split(' ');
        var date = dadosObj.dtInicial.split(' ');
        document.getElementById("dtinicial").value = date[0];
        document.getElementById("dtprevfinal").value = dateprev[0];

        document.getElementById("endereco").value = dadosObj.endereco;
        document.getElementById("bairro").value = dadosObj.bairro;
        document.getElementById("cep").value = dadosObj.cep;
        visualizar.BuscarEstado(dadosObj.estado);
        visualizar.BuscarCidade(dadosObj.cidade);
        document.getElementById("status").value = dadosObj.status;
        document.getElementById("uf").value = dadosObj.estado;

        visualizar.BuscarListaServiço(dadosObj.id);

        document.getElementById("valortotal").value = visualizar.dinheiro(dadosObj.valorTotal);
    },

    BuscarListaServiço: function (id) {
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/GerarServiço/BuscarItensServiço?id=" + id, config)
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

    BuscarFunc: function (id) {
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Funcionario/BuscarFuncionario?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {
                document.getElementById("crea").value = dadosObj.crea;
            })
            .catch(function () {
                
            })
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

    BuscarEstado: function (id) {
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

    BuscarCidade: function (id) {
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
