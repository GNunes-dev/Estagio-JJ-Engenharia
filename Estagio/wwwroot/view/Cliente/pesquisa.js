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

        fetch("/Cliente/Excluir?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                if (dadosObj.operacao) {
                    var tableT = $('#tabelaCliente').DataTable();
                    tableT.clear().destroy();
                    pesquisa.btnPesquisarOnClick();
                }

            })
            .catch(function () {
                alert("Deu erro.")
            });



    },


    btnPesquisarOnClick: function () {
        document.getElementById("tbClientes").style.display = "block";
    /*

        var tbodyClientes = document.getElementById("tbodyClientes");
        tbodyClientes.innerHTML = `<tr><td colspan="3"><img src=\"/lib/Dashboard/assets/img/ajax-loader.gif"\ />carregando...</td></tr>`
        document.getElementById("btnPesquisar").disabled = "disabled";*/

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

                var data = [];
                for (var i = 0; i < dadosObj.length; i++) {
                        data.push([
                            dadosObj[i].id,
                            dadosObj[i].nome,
                            dadosObj[i].login,
                           dadosObj[i].email,
                            dadosObj[i].cpf,
                            dadosObj[i].telefone,
                            dadosObj[i].endereco,
                            dadosObj[i].bairro,
                            '<button type="button" class="btn btn-info" onclick="pesquisa.editar(' + dadosObj[i].id + ')">Editar</button > ',
                            '<button type="button" class="btn btn-danger" onclick="pesquisa.excluir(' + dadosObj[i].id + ')">Excluir</button > '
                           // '<td><a href="javascript:void" onclick="pesquisa.editar('+ dadosObj[i].id + ')">editar</a></td>',
                           // '<td><a href="javascript:void" onclick="pesquisa.excluir('+ dadosObj[i].id + ')">excluir</a></td>'
                    ]);
                }
                $(document).ready(function () {
                    $('#tabelaCliente').DataTable({
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


                /*var linhas = "";
                for (var i = 0; i < dadosObj.length; i++) {

                    var template =
                        `<tr data-id="${dadosObj[i].id}">
                            <td>${dadosObj[i].id}</td>
                            <td>${dadosObj[i].nome}</td>
                            <td>${dadosObj[i].login}</td>
                            <td>${dadosObj[i].email}</td>
                            <td>${dadosObj[i].cpf}</td>
                            <td>${dadosObj[i].telefone}</td>                        
                            <td>${dadosObj[i].endereco}</td>
                            <td>${dadosObj[i].bairro}</td>
                            <td><a href="javascript:void" onclick="pesquisa.editar(${dadosObj[i].id})">editar</a></td>
                            <td><a href="javascript:void" onclick="pesquisa.excluir(${dadosObj[i].id})">excluir</a></td>
                         </tr>`
                    linhas += template;
                }

                if (linhas == "") {

                    linhas = `<tr><td colspan="3">Sem resultado.</td></tr>`
                }

                tbodyClientes.innerHTML = linhas;*/
            })
            .catch(function () {
                tbodyClientes.innerHTML = `<tr><td colspan="3">Deu erro...</td></tr>`
            })


    },
    editar: function (id) {
        window.location.href = "/Cliente/cadastro?id=" + id;
    }
}
pesquisa.btnPesquisarOnClick();