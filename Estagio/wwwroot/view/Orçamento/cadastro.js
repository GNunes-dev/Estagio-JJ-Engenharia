let lista = [];
let idserv = [];
let idlic = [];
let listalic = [];
let listalicremoved = [];
let listaservremoved = [];
let listalicadd = [];
let listaservadd = [];
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
            fetch("/Orçamento/BuscarOrçamento?id=" + id, config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    if (dadosObj != null)
                        cadastro.carregaOrçamento(dadosObj);
                })
                .catch(function () {
                });
        }
    },
    carregaOrçamento: function (dadosObj) {
        document.getElementById("descriçao").value = dadosObj.descriçao;
        document.getElementById("cliente").value = dadosObj.clienteId.id;
        document.getElementById("setor").value = dadosObj.setorId.id;
        document.getElementById("formapag").value = dadosObj.formaPag;
        var data = dadosObj.dtVencimento.split(' ');
        var date2 = data[0].split('/');
        var thedate = date2[2] + "-" + date2[1] + "-" + date2[0];
        document.getElementById("vencimento").value = thedate;
        document.getElementById("valortotal").value = dadosObj.valorTotal;
        cadastro.buscarServiço(dadosObj.setorId.id);
        cadastro.BuscarListaServiço(dadosObj.id);
        cadastro.BuscarListaLicença(dadosObj.id);

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

                fetch("/Orçamento/Criar", config)
                    .then(function (dadosJson) {
                        var obj = dadosJson.json(); //deserializando
                        return obj;
                    })
                    .then(function (dadosObj) {
                        if (dadosObj.msg == "") {
                            cadastro.gravarlistaserviço(dadosObj.id);
                            cadastro.gravarlistalicença(dadosObj.id);
                            alert("Orçamento cadastrado com sucesso");
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
        else {
            var config = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                credentials: 'include', //inclui cookies
                body: JSON.stringify(valida.dados)
            };
            fetch("/Orçamento/Editar", config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    if (listaservremoved.length > 0) {
                        cadastro.removeitensServ(dadosObj.id);
                    }
                        
                    if (listalicremoved.length > 0) {
                        cadastro.removeitensLic(dadosObj.id);
                    }

                    if (listalicadd.length > 0) {
                        cadastro.gravarnovalicença(dadosObj.id);
                    }

                    if (listaservadd.length > 0) {
                        cadastro.gravarnovoserviço(dadosObj.id);
                    }
     
                    alert("Orçamento Alterado com sucesso");
                    window.location.href = "/Orçamento/pesquisa";
                })
                .catch(function () {
                    document.getElementById("divMsg").innerHTML = "deu erro";
                })

        }
    },

    validaDados: function () {
        var Id = cadastro.getId();
        var Descriçao = document.getElementById("descriçao").value;
        var dtVencimento = document.getElementById("vencimento").value;
        var Cliente = document.getElementById("cliente").value;
        var Setor = document.getElementById("setor").value;
        var FormaPag = document.getElementById("formapag").value;
        var ValorTotal = document.getElementById("valortotal").value;

        var erro = "";
        if (Descriçao.trim() == "") {

            erro += "Descriçao Não informada.<br>";
        }
        if (Cliente.trim() == "") {

            erro += "Cliente Não informado.<br>";
        }
        if (dtVencimento.trim() == "") {

            erro += "data de Vencimento Não informado.<br>";
        }
        if (Setor.trim() == "") {

            erro += "Setor Não informado.<br>";
        }
        if (FormaPag.trim() == "") {

            erro += "Forma de Pagamento Não informado.<br>";
        }
        if (ValorTotal.trim() == "") {

            erro += "ValorTotal Não informado.<br>";
        }
        valida = {
            msg: erro,
            dados: {
                Id,
                Descriçao,
                Cliente,
                dtVencimento,
                Setor,
                FormaPag,
                ValorTotal
            }
        }
        return valida;

    },

    gravarlistaserviço: function (idorça) {
        var id;
        
        for (var i = 0; i < idserv.length; i++) {
            id = idserv[i];

            $.ajax({
                type: 'POST',
                url: '/Orçamento/GravarListaServiço?id=' +id + '&idorça=' + idorça,
                contentType: 'application/json',             
                success: function (res) {
                    if (res.operacao) {

                    }
                    else {
                        var erro = "";
                        erro += "<div class=\"alert alert-danger\" role=\"alert\">";
                        erro += res.msg;
                        erro += "</div>";
                        document.getElementById('divMsg').innerHTML = erro;
                    }
                }
            });
           
        }
    },

    gravarlistalicença: function (idorça) {
        var id;

        for (var i = 0; i < idlic.length; i++) {
            id = idlic[i];

            $.ajax({
                type: 'POST',
                url: '/Orçamento/GravarListaLicença?id=' + id + '&idorça=' + idorça,
                contentType: 'application/json',
                success: function (res) {
                    if (res.operacao) {

                    }
                    else {
                        var erro = "";
                        erro += "<div class=\"alert alert-danger\" role=\"alert\">";
                        erro += res.msg;
                        erro += "</div>";
                        document.getElementById('divMsg').innerHTML = erro;
                    }
                }
            });

        }
    },

    gravarnovoserviço: function (idorça) {
        var id;

        for (var i = 0; i < listaservadd.length; listaservadd.splice(0,1)) {
            $.ajax({
                type: 'POST',
                url: '/Orçamento/GravarListaServiço?id=' + listaservadd[i] + '&idorça=' + idorça,
                contentType: 'application/json',
                success: function (res) {
                    if (res.operacao) {

                    }
                    else {
                        var erro = "";
                        erro += "<div class=\"alert alert-danger\" role=\"alert\">";
                        erro += res.msg;
                        erro += "</div>";
                        document.getElementById('divMsg').innerHTML = erro;
                    }
                }
            });

        }
    },

    gravarnovalicença: function (idorça) {
        var id;

        for (var i = 0; i < listalicadd.length; listalicadd.splice(0,1)) {
            $.ajax({
                type: 'POST',
                url: '/Orçamento/GravarListaLicença?id=' + listalicadd[i] + '&idorça=' + idorça,
                contentType: 'application/json',
                success: function (res) {
                    if (res.operacao) {

                    }
                    else {
                        var erro = "";
                        erro += "<div class=\"alert alert-danger\" role=\"alert\">";
                        erro += res.msg;
                        erro += "</div>";
                        document.getElementById('divMsg').innerHTML = erro;
                    }
                }
            });

        }
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
                    idserv.push(dadosObj[i].id);
                    lista.push(dadosObj[i].nome, dadosObj[i].valor);
                    var template =
                        `<tr>
                            <td>${dadosObj[i].nome}</td>
                            <td>${dadosObj[i].valor}</td>
                            <td><a href="javascript:void" onclick="cadastro.btnRemoverServiço(${i})">excluir</a></td>
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
                    idlic.push(dadosObj[i].id);
                    listalic.push(dadosObj[i].nome, dadosObj[i].valorTotal);
                    var template =
                        `<tr>
                            <td>${dadosObj[i].nome}</td>
                            <td>${dadosObj[i].valorTotal}</td>
                            <td><a href="javascript:void" onclick="cadastro.btnRemoverLicença(${i})">excluir</a></td>
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

    buscarCliente: function () {

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Cliente/ObterTodos", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var selCliente = document.getElementById("cliente");

                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].nome}</option>`;
                }

                selCliente.innerHTML = opts;
            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })

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

    buscarFormaPag: function () {

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/FormaPagamento/ObterTodos", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var formapag = document.getElementById("formapag");

                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i]}">
                            ${dadosObj[i]}</option>`;
                }

                formapag.innerHTML = opts;
            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })
    },

    buscarServiço: function (id_set) {

        $.ajax({
            type: 'GET',
            url: '/Serviço/BuscarServiçoSetor?id_set=' + id_set,
            contentType: 'application/json',
            success: function (res) {
                if (res != null) {
                    var selServiço = document.getElementById("serviço");

                    var opts = "<option value=''></option>";
                    for (var i = 0; i < res.length; i++) {

                        opts += `<option 
                            value="${res[i].id}">
                            ${res[i].nome}</option>`;
                    }

                    selServiço.innerHTML = opts;
                }
                else {
                    document.getElementById("divMsg").innerHTML = "deu erro";
                }
            }
        });


    },

    buscarLicença: function () {

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Licença/ObterTodos", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var selLicença = document.getElementById("licença");

                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].nome}</option>`;
                }

                selLicença.innerHTML = opts;

            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })

    },

    //essa função de buscar o valor vai ficar dentro do click do botao adicionar serviço
    buscarValor: function () {
        var total = 0;
        let val;
        for (var i = 0; i + 1 < lista.length; i = i + 2) {
            val = parseInt(lista[i + 1], 10);
            total = total + val;
        }
        for (var j = 0; j + 1 < listalic.length; j = j + 2) {
            val = parseInt(listalic[j + 1], 10);
            total = total + val;
        }
        document.getElementById("valortotal").value = total;
    },

    btnAddServico: function () {

        document.getElementById("tbServiços").style.display = "block";

        var tbodyServiços = document.getElementById("tbodyServiços");

        tbodyServiços.innerHTML = `<tr><td colspan="3"><img src=\"/lib/Dashboard/assets/img/ajax-loader.gif"\ />carregando...</td></tr>`
        //document.getElementById("btnAddServico").disabled = "disabled";

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        var serviço = document.getElementById("serviço").value;

        fetch("/Serviço/ObterValor?serviço=" + serviço, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var linhas = "";
                idserv.push(dadosObj.id);
                listaservadd.push(dadosObj.id);
                lista.push(dadosObj.nome, dadosObj.valor);

                for(var i = 0; i+1<lista.length; i = i+2)
                {
                   var template =
                    `<tr>
                            <td>${lista[i]}</td>
                            <td>${lista[i+1]}</td>
                            <td><a href="javascript:void" onclick="cadastro.btnRemoverServiço(${i})">excluir</a></td>
                      </tr>`

                    linhas += template;
                }      
                   
                if (linhas == "") {

                    linhas = `<tr><td colspan="3">Sem resultado.</td></tr>`
                }
                cadastro.buscarValor();
             
                tbodyServiços.innerHTML = linhas;
            })
            .catch(function () {
                tbodyServiços.innerHTML = `<tr><td colspan="3">Adicione um Valor Valido</td></tr>`
            })
            .finally(function () {

                document.getElementById("btnAddServico").disabled = "";
            });
    },

    //RECEBE A POSIÇAO QUE DETERMINADO ID ESTA NA LISTA E APAGA   ("VALOR" É A POSIÇÃO QUE TAL ID ESTA GRAVADO NA "LISTA")
    btnRemoverServiço: function (valor) {

        var tbodyServiços = document.getElementById("tbodyServiços");
        listaservremoved.push(idserv[valor]);
        var linhas = "";
        lista.splice(valor, 2);

        var valoridserv = valor / 2;
        idserv.splice(valoridserv, 1);

                for (var i = 0; i + 1 < lista.length; i = i + 2) {
                    var template =
                        `<tr>
                            <td>${lista[i]}</td>
                            <td>${lista[i + 1]}</td>
                            <td><a href="javascript:void" onclick="cadastro.btnRemoverServiço(${i})">excluir</a></td>
                      </tr>`

                    linhas += template;
                }

                if (linhas == "") {

                    linhas = `<tr><td colspan="3">Sem resultado.</td></tr>`
                }
                cadastro.buscarValor();
                tbodyServiços.innerHTML = linhas;
    },


    btnAddLicença: function () {

        document.getElementById("tbLicenças").style.display = "block";

        var tbodyLicenças = document.getElementById("tbodyLicenças");

        tbodyLicenças.innerHTML = `<tr><td colspan="3"><img src=\"/lib/Dashboard/assets/img/ajax-loader.gif"\ />carregando...</td></tr>`
        //document.getElementById("btnAddServico").disabled = "disabled";

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        var licença = document.getElementById("licença").value;

        fetch("/Licença/ObterValor?licença=" + licença, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var linhas = "";
                idlic.push(dadosObj.id);
                listalicadd.push(dadosObj.id);
                listalic.push(dadosObj.nome, dadosObj.valorTotal);

                for (var i = 0; i + 1 < listalic.length; i = i + 2) {
                    var template =
                        `<tr>
                            <td>${listalic[i]}</td>
                            <td>${listalic[i + 1]}</td>
                            <td><a href="javascript:void" onclick="cadastro.btnRemoverLicença(${i})">excluir</a></td>
                      </tr>`

                    linhas += template;
                }

                if (linhas == "") {

                    linhas = `<tr><td colspan="3">Sem resultado.</td></tr>`
                }
                cadastro.buscarValor();

                tbodyLicenças.innerHTML = linhas;
            })
            .catch(function () {
                tbodyLicenças.innerHTML = `<tr><td colspan="3">Adicione um Valor Valido</td></tr>`
            })
            .finally(function () {

                document.getElementById("btnAddLicença").disabled = "";
            });
    },




    //RECEBE A POSIÇAO QUE DETERMINADO ID ESTA NA LISTALIC E APAGA
    btnRemoverLicença: function (valortot) {

        var tbodyLicenças = document.getElementById("tbodyLicenças");
        var linhas = "";

        listalicremoved.push(idlic[valortot]);
        listalic.splice(valortot, 2);

        var valoridlic = valortot / 2;
        idlic.splice(valoridlic, 1);

        for (var i = 0; i + 1 < listalic.length; i = i + 2) {
            var template =
                `<tr>
                            <td>${listalic[i]}</td>
                            <td>${listalic[i + 1]}</td>
                            <td><a href="javascript:void" onclick="cadastro.btnRemoverLicença(${i})">excluir</a></td>
                      </tr>`

            linhas += template;
        }

        if (linhas == "") {

            linhas = `<tr><td colspan="3">Sem resultado.</td></tr>`
        }
        cadastro.buscarValor();
        tbodyLicenças.innerHTML = linhas;
    },


    removeitensServ: function (id) {
        if (listaservremoved.length > 0) {
            for (var i = 0; i < listaservremoved.length; listaservremoved.splice(0, 1))
            {
                $.ajax({
                    type: 'DELETE',
                    url: '/Orçamento/ExcluiOrçaServ?id=' + id + '&idserv=' + listaservremoved[i],
                    contentType: 'application/json',
                    success: function (res) {
                        if (res != null) {
                        }
                    }
                });
            }
        }  
    },

    removeitensLic: function (id) {

        for (var i = 0; i < listalicremoved.length; listalicremoved.splice(0,1)) {

            $.ajax({
                type: 'DELETE',
                url: '/Orçamento/ExcluiOrçaLic?id=' + id + '&idlic=' + listalicremoved[i],
                contentType: 'application/json',
                success: function (res) {
                    if (res != null) {
                    }
                }
            });

            
        }

    }

}

cadastro.init();
cadastro.buscarCliente();
cadastro.buscarSetor();
cadastro.buscarLicença();
cadastro.buscarFormaPag();