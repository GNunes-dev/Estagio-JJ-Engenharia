var pesquisa = {

    excluir: function (id) {

        if (!confirm("Deseja excluír?")) {
            return;
        }

        var config = {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Orçamento/Excluir?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                if (dadosObj.operacao) {
                    var tableT = $('#tabelaOr').DataTable();
                    tableT.clear().destroy();
                    pesquisa.btnPesquisarOnClick();
                }

            })
            .catch(function () {
                alert("Deu erro.")
            });



    },


    btnPesquisarOnClick: function () {

        document.getElementById("tbOr").style.display = "block";

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Orçamento/ObterTodos", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var data = [];
                var dtvenc;
                for (var i = 0; i < dadosObj.length; i++) {
                    dtvenc = dadosObj[i].dtVencimento.split(' ');
                    data.push([
                            dadosObj[i].clienteId.nome,
                            dadosObj[i].descriçao,                     
                        dtvenc[0],
                        '<button type="button" class="btn btn-primary" onclick="pesquisa.visualizar(' + dadosObj[i].id + ')">Visualizar</button > ',
                        '<button type="button" class="btn btn-info" onclick="pesquisa.editar(' + dadosObj[i].id + ')">Editar</button > ',
                        '<button type="button" class="btn btn-danger" onclick="pesquisa.excluir(' + dadosObj[i].id + ')">Excluir</button > '
                    ]);
                }
                $(document).ready(function () {
                    $('#tabelaOr').DataTable({
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
                tbodyLic.innerHTML = `<tr><td colspan="3">Deu erro...</td></tr>`
            })
    },


    visualizar: function (id) {
        window.location.href = "/Orçamento/visualizar?id=" +id;          
    },
    editar: function (id) {
        window.location.href = "/Orçamento/cadastro?id=" + id;
    }

}
pesquisa.btnPesquisarOnClick();