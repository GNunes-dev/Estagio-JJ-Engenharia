function resolverDepoisDe2Segundos(x) {
    return new Promise(resolve => {
        setTimeout(() => {
            resolve(x);
        }, 200);
    });
}

var cadastro = {

    btnCadastrar:  async function () {
        var valida = cadastro.validaDados();
        if (valida.msg == "") {
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

                await cadastro.gravarPagamento(vv, c[2], Descriçao, c[0], FormaPag);
                var a = await resolverDepoisDe2Segundos(2);
            }

            alert("Novo Recebimento Cadastrado com sucesso");
            window.location.reload(true);
        }
    },

    gravarPagamento: function (valor, data, Descriçao, index, FormaPag) {
        $.ajax({
            type: 'POST',
            url: '/Recebimento/GravarPagamento?valor=' + valor + '&data=' + data + '&desc=' + Descriçao + " Parcela: " + index + '&formapag=' + FormaPag,
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
    },

    validaDados: function () {
        
        var Descriçao = document.getElementById("descriçao").value;
        var DtVencimento = document.getElementById("dtpag").value;
        var ValorTotal = document.getElementById("valortotal").value;
        var NumParcela = document.getElementById("numparcela").value;

        ValorTotal = ValorTotal.replace(/[\D]+/g, '');
        ValorTotal = ValorTotal.replace(/([0-9]{2})$/g, ".$1");
        ValorTotal = ValorTotal.replace('.', ',');
        var erro = "";
 
        if (Descriçao.trim() == "") {

            erro += "Descrição Não informado.<br>";
        }
        if (DtVencimento.trim() == "") {

            erro += "Data Vencimento Não informada.<br>";
        }
        if (ValorTotal.trim() == "") {

            erro += "ValorTotal Não informado.<br>";
        }
        if (NumParcela.trim() == "") {

            erro += "Numero de Parcelas Não informado.<br>";
        }
        valida = {
            msg: erro,
            dados: {
                Descriçao,
                DtVencimento,
                ValorTotal,
                NumParcela
            }
        }
        return valida;

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

    limpaTabela: function (tabela) {
        var tableT = $('#' + tabela).DataTable();
            tableT.clear().destroy();
    },

    insereTabela: function (tabela, data)
    {
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
data = new Date(year, month , day);
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
        datapag.setDate(datapag.getDate()+1);
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
                    i+1,
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

cadastro.buscarFormaPag();
cadastro.buscarNumParc();

$(document).ready(function () {
    //$("#quantParcelas").mask("99");
    //$("#novadata").mask("99/99/9999");
    //$("#dtpag").mask("99/99/9999");
    $("#valortotal").mask('#.##0,00', { reverse: true });
    $("#valorparc").mask('#.##0,00', { reverse: true });
});