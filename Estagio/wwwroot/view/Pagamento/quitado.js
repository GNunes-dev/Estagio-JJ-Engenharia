var quitado = {

    btnQuitadoOnClick: function () {

        document.getElementById("tbQuitados").style.display = "block";

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
                var datapag;
                var j = 1;
                for (var i = 0; i < dadosObj.length; i++)
                {
                    if (dadosObj[i].quitado == true)
                    {
                        date = dadosObj[i].dtVencimento.split(' ');
                        datapag = dadosObj[i].dtPagamento.split(' ');
                        var valorParcela = parseFloat(dadosObj[i].valor);
                        var parcial = parseFloat(dadosObj[i].valorParcial);
                        var desc = dadosObj[i].descriçao;
                        data.push([
                            j,
                            dadosObj[i].descriçao,
                            dadosObj[i].formaPag,
                            date[0],
                            datapag[0],
                            quitado.dinheiro(valorParcela),
                            quitado.dinheiro(parcial),
                            '<button type="button" class="btn btn-primary"  onclick="quitado.extornar(' + dadosObj[i].id + ')">Estornar</button >'           
                        ]);
                        j++;
                    }
                }
                $(document).ready(function () {
                    $('#tabelaQuitados').DataTable({
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
                tbodyQuitados.innerHTML = `<tr><td colspan="3">Deu erro...</td></tr>`
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

    extornar: function (id) {

            $.ajax({
                type: 'POST',
                url: '/Pagamento/Extornar?id=' + id,
                contentType: 'application/json',
                success: function (res) {
                    if (res.operacao) {
                        alert("Parcela extornada com sucesso");
                        var tableT = $('#tabelaQuitados').DataTable();
                        tableT.clear().destroy();
                        quitado.btnQuitadoOnClick();
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

}
quitado.btnQuitadoOnClick();

$(document).ready(function () {
    //$("#quantParcelas").mask("99");
    //$("#novadata").mask("99/99/9999");
    //$("#dtpag").mask("99/99/9999");
    //$("#valortotal").mask('#.##0,00', { reverse: true });
    $("#valorparc").mask('#.##0,00', { reverse: true });
});