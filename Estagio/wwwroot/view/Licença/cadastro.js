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
            fetch("/Licença/BuscarLicença?id=" + id, config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    if (dadosObj != null)
                        cadastro.carregaLicença(dadosObj);
                })
                .catch(function () {
                });
        }
    },
    carregaLicença: function (dadosObj) {
        document.getElementById("nome").value = dadosObj.Nome;
        document.getElementById("cliente").value = dadosObj.clienteId.id;
        document.getElementById("crea").value = dadosObj.Crea;
        document.getElementById("orgao").value = dadosObj.orgaoId;
        document.getElementById("setor").value = dadosObj.setorId;
        document.getElementById("valortotal").value = dadosObj.valorTotal;
        document.getElementById("numprocesso").value = dadosObj.numProcesso;
        document.getElementById("numlicença").value = dadosObj.numLicença;
        document.getElementById("versao").value = dadosObj.Versao;
        document.getElementById("cnae").value = dadosObj.Funcionario.id;
        document.getElementById("dtinicial").value = dadosObj.dtInicial;
        document.getElementById("vencimento").value = dadosObj.dtVencimento;
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

                fetch("/Licença/Criar", config)
                    .then(function (dadosJson) {
                        var obj = dadosJson.json(); //deserializando
                        return obj;
                    })
                    .then(function (dadosObj) {
                        if (dadosObj.msg == "") {
                            cadastro.salvarDocumento(dadosObj.id);
                            cadastro.gravarlistaPagamento();
                            alert("Licença cadastrada com sucesso");
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
            fetch("/Licença/Editar", config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    alert("Licença Alterada com sucesso");
                    window.location.href = "/Licença/pesquisa";
                })
                .catch(function () {
                    document.getElementById("divMsg").innerHTML = "deu erro";
                })

        }
    },



    validaDados: function () {
        var Id = cadastro.getId();
        var Nome = document.getElementById("nome").value;
        var Cliente = document.getElementById("cliente").value;
        var Crea = document.getElementById("crea").value;
        var Orgao = document.getElementById("orgao").value;
        var Setor = document.getElementById("setor").value;
        var valorTotal = document.getElementById("valortotal").value;
        var numProcesso = document.getElementById("numprocesso").value;
        var numLicença = document.getElementById("numlicença").value;
        var Versao = document.getElementById("versao").value;
        var Cnae = document.getElementById("cnae").value;
        var dtInicial = document.getElementById("dtinicial").value;
        var dtVencimento = document.getElementById("vencimento").value;
        var erro = "";

        valorTotal = valorTotal.replace(/[\D]+/g, '');
        valorTotal = valorTotal.replace(/([0-9]{2})$/g, ".$1");
        valorTotal = valorTotal.replace('.', ',');
        if (Nome.trim() == "") {

            erro += "Nome Não informado.<br>";
        }
        if (Cliente.trim() == "") {

            erro += "Cliente Não informado.<br>";
        }
        if (Crea.trim() == "") {

            erro += "Crea Não informado.<br>";
        }
        if (Orgao.trim() == "") {

            erro += "Orgao Não informado.<br>";
        }
        if (Setor.trim() == "") {

            erro += "Setor Não informado.<br>";
        }
        if (valorTotal.trim() == "") {

            erro += "Valor Não informado.<br>";
        }
        if (numProcesso.trim() == "") {

            erro += "Numero de Processo Não informado.<br>";
        }
        if (numLicença.trim() == "") {

            erro += "Numero de Licença Não informado.<br>";
        }
        if (Versao.trim() == "") {

            erro += "Versao Não informado.<br>";
        }
        if (dtInicial.trim() == "") {

            erro += "Data Inicial Não informada.<br>";
        }
        if (Cnae.trim() == "") {

            erro += "Cnae Não informado.<br>";
        }
        if (dtVencimento.trim() == "") {

            erro += "Data de Vencimento Não informado.<br>";
        }
        valida = {
            msg: erro,
            dados: {
                Id,
                Nome,
                Cliente,
                Crea,
                Orgao,
                Setor,
                valorTotal,
                numProcesso,
                numLicença,
                Versao,
                dtInicial,
                Cnae,
                dtVencimento
            }
        }
        return valida;

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

    buscarFuncionario: function () {

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Funcionario/ObterTodos", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var func = document.getElementById("crea");

                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].crea}</option>`;
                }

                func.innerHTML = opts;
            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })

    },

    buscarOrgao: function () {

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/OrgaoLicenciamento/ObterTodos", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var selOrgao = document.getElementById("orgao");

                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].descriçao}</option>`;
                }

                selOrgao.innerHTML = opts;
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

    buscarValor: function (orgao) {

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui 
        };

        fetch("/OrgaoLicenciamento/ObterValor?orgao=" + orgao, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {
                var din = cadastro.dinheiro(dadosObj.valor);
                document.getElementById("valortotal").value = din;
            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })
    },

    salvarDocumento: function (id) {
        var fd = new FormData();

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
    },

    limpaTabela: function (tabela) {
        var tableT = $('#' + tabela).DataTable();
        tableT.clear().destroy();
    },

    insereTabela: function (tabela, data) {
        $(document).ready(function () {
            $('#' + tabela).DataTable({
                data: data,
                "responsive": true,
                "autoWidth": false,
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
                },
                "pageLength": 6,
                responsive: {
                    details: {
                        display: $.fn.dataTable.Responsive.display.childRowImmediate
                    }
                }
            });
        });

    },

    DataEN: function (data) {

        const dataSplit = data.split('/');
        var day = dataSplit[0]; // 15
        var month = dataSplit[1]; // 04
        var year = dataSplit[2]; // 2019
        data = new Date(year, month, day);
        return data;
    },

    gerarParcela: async function () {


        var valor = $('#valortotal').val();
        if (valor != "") {
            valor = valor.replace(/[\D]+/g, '');
            valor = valor.replace(/([0-9]{2})$/g, ".$1");
        }
        var parcelas = $('#numparcela').val();
        var datepag = document.getElementById("dtpag").value;
        var datapag = new Date(datepag);
        datapag.setDate(datapag.getDate() + 1);
        var hoje = new Date();

        if (datapag != null) {
            hoje = new Date(datapag).toLocaleDateString();
        }
        else {
            alert("escolha uma data valida");

        }

        if (valor == "" || datepag == "") {
            alert("escolha data de pagamento e tenha um valor para dividir");
            document.getElementById("numparcela").value = "";
        }
        else {
            cadastro.limpaTabela("tabelaParcela");
            document.getElementById("tbParcela").style.display = "block";
            var valorParcela = parseFloat(valor) / parseInt(parcelas);
            var arredondar = valorParcela.toFixed(2);
            var data = [];

            if (valor != "")
                for (var i = 0; i < parseInt(parcelas); i++) {
                    data.push([
                        i + 1,
                        cadastro.dinheiro(arredondar),
                        hoje,
                        '<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" onclick="cadastro.editarTabela(' + parseInt(i + 1) + ')">Editar</button > '
                        //'<button class="btn btn-primary btn-sm" onclick="cadastro.editarTabela(' + parseInt(i + 1) + ')"><i class="fas fa-edit"/></button>'
                    ]);
                    datapag.setMonth(datapag.getMonth() + 1);
                    hoje = new Date(datapag).toLocaleDateString();
                }
            cadastro.insereTabela('tabelaParcela', data);
        }
    },

    getData: function (index) {
        var table = $('#tabelaParcela').DataTable();
        var quant = table.column(0).data().length;
        var nValor = 0.0;
        for (var i = 0; i < quant; i++) {
            var rows = table.rows(i).data();
            var c = rows[0];
            if (parseInt(c[0]) == parseInt(index))
                return c[2];
        }
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

    alterartabela: function () {
        var table = $('#tabelaParcela').DataTable();
        var index = localStorage["numlinha"];
        var novadata = $('#novadata').val();
        var parc = $('#valorparc').val();
        //localStorage["numlinha"] = index;

        var ultimo = false;
        var total = $('#valortotal').val();
        total = total.replace(/[\D]+/g, '');
        total = total.replace(/([0-9]{2})$/g, ".$1");
        var mudou = 0;
        var valor = parc;

        var datapag = new Date(novadata);
        datapag.setDate(datapag.getDate() + 1);
        var data_recebimento = new Date();
        data_recebimento = new Date(datapag).toLocaleDateString();

        var d = [];
        valor = valor.replace(/[\D]+/g, '');
        valor = valor.replace(/([0-9]{2})$/g, ".$1");
        var svalor = 0.0;
        var quant = table.column(0).data().length;
        var nValor = 0.0;

        for (var i = 0; i < quant; i++) {
            var rows = table.rows(i).data();
            var c = rows[0];
            var vv = c[1];
            vv = vv.replace(/[\D]+/g, '');
            vv = vv.replace(/([0-9]{2})$/g, ".$1");


            if (parseInt(c[0]) != index) {
                if (mudou == 0) {
                    svalor += parseFloat(vv);
                    d.push([
                        c[0],
                        c[1],
                        c[2],
                        c[3]
                    ]);
                }
                else {
                    d.push([
                        c[0],
                        cadastro.dinheiro(nValor),
                        c[2],
                        c[3],
                        c[4]
                    ]);
                }

            }
            else {
                if (index == quant)
                    ultimo = true;
                mudou = 1;
                d.push([
                    index,
                    cadastro.dinheiro(valor),
                    data_recebimento,
                    '<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" onclick="cadastro.editarTabela(' + index + ')">Editar</button > '
                ]);
                svalor += parseFloat(valor);
                nValor = (parseFloat(total) - parseFloat(svalor)) / (parseInt(quant) - (i + 1));
                var y = 0;
            }
        }
        if (ultimo == false) {
            cadastro.limpaTabela('tabelaParcela');
            cadastro.insereTabela('tabelaParcela', d);
        } else {
            alert('Não é possivel alterar a ultima parcela!');
        }
    },

    editarTabela: function (index) {
        localStorage["numlinha"] = index;
    },

    gravarlistaPagamento: function () {

        var table = $('#tabelaParcela').DataTable();
        var quant = $('#numparcela').val();
        var Nome = document.getElementById("nome").value;
        var FormaPag = document.getElementById("formapag").value;
        var rows;
        var c;

        for (var i = 0; i < parseInt(quant); i++) {
            rows = table.rows(i).data();
            c = rows[0];
            var vv = c[1];
            vv = vv.replace(/[\D]+/g, '');
            vv = vv.replace(/([0-9]{2})$/g, ".$1");

            $.ajax({
                type: 'POST',
                url: '/Recebimento/GravarPagamento?valor=' + vv + '&data=' + c[2] + '&desc=' + Nome + " Parcela: " + c[0] + '&formapag=' + FormaPag,
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

    buscarNumParc: function () {
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/FormaPagamento/NumeroParcelas", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var num = document.getElementById("numparcela");

                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i]}">
                            ${dadosObj[i]}</option>`;
                }

                num.innerHTML = opts;
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
}

cadastro.init();
cadastro.buscarCliente();
cadastro.buscarFuncionario();
cadastro.buscarOrgao();
cadastro.buscarSetor();
cadastro.buscarFormaPag();
cadastro.buscarNumParc();

$(document).ready(function () {
    //$("#quantParcelas").mask("99");
    //$("#novadata").mask("99/99/9999");
    //$("#dtpag").mask("99/99/9999");
    $("#valortotal").mask('#.##0,00', { reverse: true });
    $("#valorparc").mask('#.##0,00', { reverse: true });
});