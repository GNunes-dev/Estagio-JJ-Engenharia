let lista = [];
let idserv = [];
let idlic = [];
let listalic = [];
var cadastro = {
    init: function () {
        cadastro.buscarEstados();
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
            fetch("/GerarServiço/BuscarServiço?id=" + id, config)
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
        document.getElementById("cliente").value = dadosObj.clienteId.id;
        document.getElementById("crea").value = dadosObj.funcionarioId;
        document.getElementById("setor").value = dadosObj.setorId;
        document.getElementById("descriçao").value = dadosObj.descriçao;
        document.getElementById("dtinicial").value = dadosObj.dtInicio;
        document.getElementById("dtprevfinal").value = dadosObj.dtPrevFim;
        document.getElementById("endereco").value = dadosObj.endereco;
        document.getElementById("bairro").value = dadosObj.bairro;
        document.getElementById("cep").value = dadosObj.cep;
        document.getElementById("status").value = dadosObj.status;

        cadastro.buscarEstados(dadosObj.estado);
        document.getElementById("uf").value = dadosObj.estado;
        cadastro.buscarCidades(dadosObj.estado, dadosObj.cidade);

        cadastro.BuscarListaServiço(dadosObj.id);

        document.getElementById("valortotal").value = dadosObj.valorTotal;

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

                fetch("/GerarServiço/Criar", config)
                    .then(function (dadosJson) {
                        var obj = dadosJson.json(); //deserializando
                        return obj;
                    })
                    .then(function (dadosObj) {
                        if (dadosObj.msg == "") {
                            cadastro.gravarlistaserviço(dadosObj.id); //ver se ta pegando o id do orçamento certo
                            cadastro.gravarlistaPagamento();
                            alert("Novo serviço cadastrado com sucesso");
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
            fetch("/GerarServiço/Editar", config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    alert("Serviço Alterado com sucesso");
                    window.location.href = "/GerarServiço/pesquisa";
                })
                .catch(function () {
                    document.getElementById("divMsg").innerHTML = "deu erro";
                })

        }
    },

    validaDados: function () {
        var Id = cadastro.getId();
        var Cliente = document.getElementById("cliente").value;
        var Funcionario = document.getElementById("crea").value;
        var Setor = document.getElementById("setor").value;
        var Descriçao = document.getElementById("descriçao").value;
        var dtInicial = document.getElementById("dtinicial").value;
        var dtPrevFinal = document.getElementById("dtprevfinal").value;
        var Endereco = document.getElementById("endereco").value;
        var Bairro = document.getElementById("bairro").value;
        var Cep = document.getElementById("cep").value;
        var Estado = document.getElementById("uf").value;
        var Cidade = document.getElementById("selCidade").value;
        var ValorTotal = document.getElementById("valortotal").value;
        var Status = document.getElementById("status").value;

        ValorTotal = ValorTotal.replace(/[\D]+/g, '');
        ValorTotal = ValorTotal.replace(/([0-9]{2})$/g, ".$1");
        ValorTotal = ValorTotal.replace('.', ',');
        var erro = "";
        if (Cliente.trim() == "") {

            erro += "Cliente Não informada.<br>";
        }
        if (Funcionario.trim() == "") {

            erro += "Crea Não informado.<br>";
        }
        if (Setor.trim() == "") {

            erro += "Setor Não informado.<br>";
        }
        if (Descriçao.trim() == "") {

            erro += "Descrição Não informada.<br>";
        }
        if (dtInicial.trim() == "") {

            erro += "Data Inicial Não informada.<br>";
        }
        if (dtPrevFinal.trim() == "") {

            erro += "Data Prevista Não informada.<br>";
        }
        if (Endereco.trim() == "") {

            erro += "Endereco Não informado.<br>";
        }
        if (Bairro.trim() == "") {

            erro += "Bairro Não informado.<br>";
        }
        if (Cep.trim() == "") {

            erro += "Cep Não informado.<br>";
        }
        if (Estado.trim() == "") {

            erro += "Estado Não informado.<br>";
        }
        if (Cidade.trim() == "") {

            erro += "Cidade Não informado.<br>";
        }
        if (ValorTotal.trim() == "") {

            erro += "ValorTotal Não informado.<br>";
        }
        if (Status.trim() == "") {

            erro += "Status Não informado.<br>";
        }
        valida = {
            msg: erro,
            dados: {
                Id,
                Cliente,
                Funcionario,
                Setor,
                Descriçao,
                dtInicial,
                dtPrevFinal,
                Endereco,
                Bairro,
                Cep,
                Estado,
                Cidade,
                ValorTotal,
                Status
            }
        }
        return valida;

    },

    gravarlistaserviço: function (id_gerarserv) {
        var id_servico;

        for (var i = 0; i < idserv.length; i++) {
            id_servico = idserv[i];

            $.ajax({
                type: 'POST',
                url: '/GerarServiço/GravarListaServiço?id_servico=' + id_servico + '&id_gerarserv=' + id_gerarserv,
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

                var selFunc = document.getElementById("crea");

                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].crea}</option>`;
                }

                selFunc.innerHTML = opts;
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


    buscarEstados: function (id) {

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Cidade/ObterEstados", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var selUF = document.getElementById("uf");
                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].nome}</option>`;
                }

                selUF.innerHTML = opts;
                document.getElementById("uf").value = id;
            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })

    },

    buscarCidades: function (uf, cidade) {


        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui 
        };

        fetch("/Cidade/ObterCidades?uf=" + uf, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var selCidade = document.getElementById("selCidade");
                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].nome}</option>`;
                }

                selCidade.innerHTML = opts;
                document.getElementById("selCidade").value = cidade;
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
        document.getElementById("valortotal").value = cadastro.dinheiro(total);
    },

    BuscarListaServiço: function(id) {
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
                lista.push(dadosObj.nome, dadosObj.valor);

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
            })
            .catch(function () {
                tbodyServiços.innerHTML = `<tr><td colspan="3">Deu erro...</td></tr>`
            })
            .finally(function () {

                document.getElementById("btnAddServico").disabled = "";
            });
    },

    btnRemoverServiço: function (valor) {

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
        var Descriçao = document.getElementById("descriçao").value;
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
                url: '/Recebimento/GravarPagamento?valor=' + vv + '&data=' + c[2] + '&desc=' + Descriçao + " Parcela: " + c[0] + '&formapag=' + FormaPag,
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
}

cadastro.init();
cadastro.buscarCliente();
cadastro.buscarSetor();
cadastro.buscarFuncionario();
cadastro.buscarFormaPag();
cadastro.buscarNumParc();

$(document).ready(function () {
    //$("#quantParcelas").mask("99");
    //$("#novadata").mask("99/99/9999");
    //$("#dtpag").mask("99/99/9999");
    $("#valortotal").mask('#.##0,00', { reverse: true });
    $("#valorparc").mask('#.##0,00', { reverse: true });
});