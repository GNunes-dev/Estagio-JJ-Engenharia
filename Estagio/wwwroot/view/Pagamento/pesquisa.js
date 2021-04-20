var pesquisa = {

    btnPesquisarOnClick: function () {

        document.getElementById("tbPagamento").style.display = "block";

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Pagamento/ObterTodos", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var data = [];
                var date; 
                var j = 1;
                for (var i = 0; i < dadosObj.length; i++)
                {
                    if (dadosObj[i].quitado == false)
                    {
                        date = dadosObj[i].dtVencimento.split(' ');
                        var valorParcela = parseFloat(dadosObj[i].valor);
                        var parcial = parseFloat(dadosObj[i].valorParcial);
                        var arredondar = valorParcela - parcial;
                        data.push([
                            j,
                            dadosObj[i].descriçao,
                            dadosObj[i].formaPag,
                            date[0],
                            pesquisa.dinheiro(arredondar),
                            '<button type="button" class="btn btn-primary"  onclick="pesquisa.quitar(' + dadosObj[i].id + ',' + dadosObj[i].valor + ')">Quitar</button > ',
                            '<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" onclick="pesquisa.quitarparcial(' + dadosObj[i].id + ',' + j + ')">Quitar Parcial</button > '
                            //'<td><a href="javascript:void" onclick="pesquisa.quitar(' + dadosObj[i].id + ')">Quitar</a></td>',
                          // '<td><a href="javascript:void" onclick="pesquisa.quitarparcial(' + dadosObj[i].id + ')">Quitar Parcial</a></td>'                          
                        ]);
                        j++;
                    }
                }
                $(document).ready(function () {
                    $('#tabelaPagamento').DataTable({
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
            })
            .catch(function () {
                tbodyPagamento.innerHTML = `<tr><td colspan="3">Deu erro...</td></tr>`
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

    quitar: function (id, valor) {
        $.ajax({
            type: 'POST',
            url: '/Pagamento/Quitar?id=' + id + '&valor=' + valor,
            contentType: 'application/json',
            success: function (res) {
                if (res.operacao) {
                    alert("Parcela quitada com sucesso");
                    var tableT = $('#tabelaPagamento').DataTable();
                    tableT.clear().destroy();
                    pesquisa.btnPesquisarOnClick();
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

    alterartabela: function () {
        var table = $('#tabelaPagamento').DataTable();
        var index = localStorage["numlinha"];
        var id = localStorage["id"];
        var parc = $('#valorparc').val();
        parc = parc.replace(/[\D]+/g, '');
        parc = parc.replace(/([0-9]{2})$/g, ".$1");
        var quant = table.column(0).data().length;
        var rows = table.rows(0).data();
        var c = rows[0];

        for (var i = 0; i < quant && c[0] != index;i++) {
            var rows = table.rows(i).data();
            var c = rows[0];
        }
        var total = c[4];
        total = total.replace(/[\D]+/g, '');
        total = total.replace(/([0-9]{2})$/g, ".$1");
        var novo = parc;
        //fazer somar os pagamento parcial
        var nValor = (parseFloat(total) - parseFloat(parc));

        if (nValor <= 0) {
            alert("Digite um valor menor que a parcela");
        }
        else {
            
            $.ajax({
                type: 'POST',
                url: '/Pagamento/QuitarParcial?valorparcial=' + parc + '&id=' + id,
                contentType: 'application/json',
                success: function (res) {
                    if (res.operacao) {
                        alert("Quitado Parcialmente com sucesso");
                        var tableT = $('#tabelaPagamento').DataTable();
                        tableT.clear().destroy();
                        pesquisa.btnPesquisarOnClick();
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

    quitarparcial: function (id, index) {
        localStorage["numlinha"] = index;
        localStorage["id"] = id;
    },

}
pesquisa.btnPesquisarOnClick();

$(document).ready(function () {
    //$("#quantParcelas").mask("99");
    //$("#novadata").mask("99/99/9999");
    //$("#dtpag").mask("99/99/9999");
    //$("#valortotal").mask('#.##0,00', { reverse: true });
    $("#valorparc").mask('#.##0,00', { reverse: true });
});