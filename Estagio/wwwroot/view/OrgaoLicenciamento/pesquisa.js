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

        fetch("/OrgaoLicenciamento/Excluir?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                if (dadosObj.operacao) {
                    var tableT = $('#tabelaOrgao').DataTable();
                    tableT.clear().destroy();
                    pesquisa.btnPesquisarOnClick();
                }

            })
            .catch(function () {
                alert("Deu erro.")
            });



    },


    btnPesquisarOnClick: function () {

        document.getElementById("tbOrgaos").style.display = "block";

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

                var data = [];
                for (var i = 0; i < dadosObj.length; i++) {

                    data.push([
                            dadosObj[i].id,
                            dadosObj[i].descriçao,
                            dadosObj[i].valor,
                        '<button type="button" class="btn btn-info" onclick="pesquisa.editar(' + dadosObj[i].id + ')">Editar</button > ',
                        '<button type="button" class="btn btn-danger" onclick="pesquisa.excluir(' + dadosObj[i].id + ')">Excluir</button > '
                    ]);
                }
                $(document).ready(function () {
                    $('#tabelaOrgao').DataTable({
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
                tbodyOrgaos.innerHTML = `<tr><td colspan="3">Deu erro...</td></tr>`
            })


    },

    editar: function (id) {
        window.location.href = "/OrgaoLicenciamento/cadastro?id=" + id;
    }

}
pesquisa.btnPesquisarOnClick();