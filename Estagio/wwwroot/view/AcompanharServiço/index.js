var pesquisa = {
    getId: function () {
        $.ajax({
            type: 'POST',
            url: '/HomeCli/pegaId',
            contentType: 'application/json',
            success: function (res) {
                if (res > 0) {
                    pesquisa.Pesquisar(res);
                }
                else {
                    var erro = "";
                    erro += "<div class=\"alert alert-danger\" role=\"alert\">";
                    erro += "erro ao pegar id";
                    erro += "</div>";
                    document.getElementById('divMsg').innerHTML = erro;
                }
            }
        });
    },
    Pesquisar: function (id) {

        document.getElementById("tbAcompServ").style.display = "block";

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/GerarServiço/BuscarServiçoCli?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var data = [];
                var dateprev;
                var date;
                var datafinal;
                for (var i = 0; i < dadosObj.length; i++) {
                    dateprev = dadosObj[i].dtPrevFim.split(' ');
                    date = dadosObj[i].dtInicio.split(' ');
                    if (dadosObj[i].dtFim == "")
                        datafinal = "Ainda não Finalizado";
                    else {
                        datafinal = dadosObj[i].dtFim.split(' ');
                        datafinal = datafinal[0];
                    }
                    data.push([
                        dadosObj[i].clienteId.nome,
                        date[0],
                        dateprev[0],
                        datafinal,
                        pesquisa.dinheiro(dadosObj[i].valorTotal),
                        '<button type="button" class="btn btn-primary" onclick="pesquisa.visualizar(' + dadosObj[i].id + ')">Ver Tudo</button > '
                    ]);

                }
                $(document).ready(function () {
                    $('#tabelaAcompServ').DataTable({
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
                tbodyAcompServ.innerHTML = `<tr><td colspan="3">Deu erro...</td></tr>`
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

    visualizar: function (id) {
        window.location.href = "/AcompanharServiço/visualizar?id=" + id;
    }

}
pesquisa.getId();
