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

        fetch("/GerarServiço/Excluir?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                if (dadosObj.operacao) {
                    var tableT = $('#tabelaGerarServ').DataTable();
                    tableT.clear().destroy();
                    pesquisa.btnPesquisarOnClick();
                }

            })
            .catch(function () {
                alert("Deu erro.")
            });



    },


    btnPesquisarOnClick: function () {

        document.getElementById("tbGerarServ").style.display = "block";

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/GerarServiço/ObterTodos", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var data = [];
                var dateprev;
                var date; 
                for (var i = 0; i < dadosObj.length; i++) {
                    dateprev = dadosObj[i].dtPrevFim.split(' ');
                    date = dadosObj[i].dtInicio.split(' ');
                        data.push([
                            dadosObj[i].clienteId.nome,
                            date[0],
                            dateprev[0],
                            dadosObj[i].valorTotal,
                            '<button type="button" class="btn btn-primary" onclick="pesquisa.visualizar(' + dadosObj[i].id + ')">Visualizar</button > ',
                            '<button type="button" class="btn btn-info" onclick="pesquisa.atualizar(' + dadosObj[i].id + ')">Atualizar</button > ',
                            '<button type="button" class="btn btn-danger" onclick="pesquisa.excluir(' + dadosObj[i].id + ')">Excluir</button > '
                        ]);
                                     
                }
                $(document).ready(function () {
                    $('#tabelaGerarServ').DataTable({
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
                tbodyGerarServ.innerHTML = `<tr><td colspan="3">Deu erro...</td></tr>`
            })
    },


    visualizar: function (id) {
        window.location.href = "/GerarServiço/visualizar?id=" +id;          
    },

    atualizar: function (id) {
        window.location.href = "/GerarServiço/atualizar?id=" + id;
    }

}
pesquisa.btnPesquisarOnClick();