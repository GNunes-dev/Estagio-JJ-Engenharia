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

        fetch("/Projeto/Excluir?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                if (dadosObj.operacao) {
                    var tableT = $('#tabelaProjeto').DataTable();
                    tableT.clear().destroy();
                    pesquisa.btnPesquisarOnClick();
                }

            })
            .catch(function () {
                alert("Deu erro.")
            });



    },


    btnPesquisarOnClick: function () {

        document.getElementById("tbProjeto").style.display = "block";

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Projeto/ObterTodos", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var data = [];
                var dateprev;
                var date; 
                for (var i = 0; i < dadosObj.length; i++) {
                    dateprev = dadosObj[i].dtPrevFinal.split(' ');
                    date = dadosObj[i].dtInicial.split(' ');
                        data.push([
                            dadosObj[i].cliente.nome,
                            dadosObj[i].descriçao,
                            date[0],
                            dateprev[0],                  
                            '<button type="button" class="btn btn-primary" onclick="pesquisa.visualizar(' + dadosObj[i].id + ')">Visualizar</button > ',
                            '<button type="button" class="btn btn-info" onclick="pesquisa.atualizar(' + dadosObj[i].id + ')">Atualizar</button > ',
                            '<button type="button" class="btn btn-danger" onclick="pesquisa.excluir(' + dadosObj[i].id + ')">Excluir</button > '
                        ]);
                                     
                }
                $(document).ready(function () {
                    $('#tabelaProjeto').DataTable({
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
                tbodyProjeto.innerHTML = `<tr><td colspan="3">Deu erro...</td></tr>`
            })
    },


    visualizar: function (id) {
        window.location.href = "/Projeto/visualizar?id=" +id;          
    },

    atualizar: function (id) {
        window.location.href = "/Projeto/atualizar?id=" + id;
    }

}
pesquisa.btnPesquisarOnClick();