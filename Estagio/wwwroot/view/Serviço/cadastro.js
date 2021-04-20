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
            fetch("/Serviço/BuscarServiço?id=" + id, config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    if (dadosObj != null)
                        cadastro.carregaServiço(dadosObj);
                })
                .catch(function () {
                });
        }
    },
    carregaServiço: function (dadosObj) {
        document.getElementById("nome").value = dadosObj.nome;
        document.getElementById("descriçao").value = dadosObj.descriçao;
        document.getElementById("valor").value = dadosObj.valor;
        document.getElementById("setor").value = dadosObj.idSetor.id;
        document.getElementById("btnCadastrar").value = "Alterar";
    },


    btnCadastrar: function () {
        var valida = cadastro.validaDados();
        if (btnCadastrar.value != "Alterar") {
            if (valida.msg == "") {

                var config = {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json; charset=utf-8"
                    },
                    credentials: 'include', //inclui cookies
                    body: JSON.stringify(valida.dados)  //serializa
                };

                fetch("/Serviço/Criar", config)
                    .then(function (dadosJson) {
                        var obj = dadosJson.json(); //deserializando
                        return obj;
                    })
                    .then(function (dadosObj) {
                        if (dadosObj.msg == "") {
                            alert("Serviço cadastrado com sucesso");
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
                else
        {
            var config = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                credentials: 'include', //inclui cookies
                body: JSON.stringify(valida.dados)
            };
            fetch("/Serviço/Editar", config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    alert("Serviço Alterado com sucesso");
                    window.location.href = "/Serviço/pesquisa";
                })
                .catch(function () {
                    document.getElementById("divMsg").innerHTML = "deu erro";
                })
    
}
    },

    buscarSetor: function () {

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Setor/ObterTodos", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var selSetor = document.getElementById("setor");

                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].descriçao}</option>`;
                }

                selSetor.innerHTML = opts;
            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })

    },


    validaDados: function () {
        var Id = cadastro.getId();
        var Nome = document.getElementById("nome").value;
        var Descriçao = document.getElementById("descriçao").value;
        var Valor = document.getElementById("valor").value;
        var IdSetor = document.getElementById("setor").value;

        var erro = "";
        if (Nome.trim() == "") {

            erro += "Nome Não informado.<br>";
        }
        if (Descriçao.trim() == "") {

            erro += "Descrição Não informada.<br>";
        }
        if (Valor.trim() == "") {

            erro += "Valor Não informado.<br>";
        }
        if (IdSetor.trim() == "") {

            erro += "Setor Não informado.<br>";
        }
        valida = {
            msg: erro,
            dados: {
                Id,
                Nome,
                Descriçao,
                Valor,
                IdSetor
            }
        }
        return valida;

    }
}
cadastro.buscarSetor();
cadastro.init();