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
            fetch("/Licença/BuscarLicença?id=" + id, config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    if (dadosObj != null)
                        atualizar.carregaLicença(dadosObj);
                })
                .catch(function () {
                });
        }
    },
    carregaLicença: function (dadosObj) {
        var data = dadosObj.dtVencimento.split(' ');
        var date2 = data[0].split('/');
        var thedate = date2[2] + "-" + date2[1] + "-" + date2[0];
        document.getElementById("vencimento").value = thedate;
        document.getElementById("versao").value = dadosObj.versao;
       // var arquivo = document.getElementById("documento").files[0];
       // var id = atualizar.getId();
        //atualizar.BuscaArquivo(id);
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
            fetch("/Licença/Att?id=" + id, config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    alert("Licença Atualizada com sucesso");
                    window.location.href = "/Licença/pesquisa";
                })
                .catch(function () {
                    document.getElementById("divMsg").innerHTML = "deu erro";
                })

        }
    },

    convertedate: function (date) {
        $.ajax({
            type: 'GET',
            url: '/Licença/convertedate?date=' + date,
            contentType: 'application/json',
            success: function (res) {
                if (res != null) {
                    return res.date;
                }
                else {
                    return null;
                }
            }
        });
    },

    validaDados: function () {
        var Versao = document.getElementById("versao").value;
        var dtVencimento = document.getElementById("vencimento").value;
        var erro = "";
        if (Versao.trim() == "") {

            erro += "Versao Não informado.<br>";
        }
        if (dtVencimento.trim() == "") {

            erro += "Data de Vencimento Não informado.<br>";
        }
        valida = {
            msg: erro,
            dados: {
                dtVencimento,
                Versao
            }
        }
        return valida;

    },

    BuscarArquivo: function () {
        var id = atualizar.getId();
        $.ajax({
            type: 'GET',
            url: '/Licença/GetArquivo?id=' + id,
            contentType: 'application/json',
            success: function (res) {
                if (res != null) {
                    alert("retorno algo");//levar para alguma tela de visualizar arquivo enviando o arquivo?
                }
                else {
                    alert("Não existe documento cadastrado");
                }
            }
        });
    },

    salvarDocumento: function () {
        var fd = new FormData();
        var id = atualizar.getId();
        var arquivo = document.getElementById("documento").files[0];

        fd.append("id", id);
        fd.append("arquivo", arquivo);

        var configFD = {
            method: "POST",
            headers: {
                "Accept": "application/json"
            },
            body: fd
        }

        fetch("/Licença/AtualizarDoc", configFD)
            .then(function (res) {
                var obj = res.json(); //deserializando
                return obj;
            })
            .then(function (res) {
                if (res.ok) {
                }
            })
            .catch(function () {
                document.getElementById("divMsg").innerHTML = "deu erro";
            })
    }
}

atualizar.init();
