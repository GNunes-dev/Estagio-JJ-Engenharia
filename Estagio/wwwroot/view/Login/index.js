//padrão módulo para organizar o código
let index = {

    btnEntrarOnClick: function () {

        let nomeUsuario = document.getElementById("login").value;
        let senhaUsuario = document.getElementById("senha").value;

        let dados = {

            login: nomeUsuario,
            senha: senhaUsuario
        }

        var config = {
            method: "POST",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include',
            body: JSON.stringify(dados)  //serializa
        };

        fetch("/Login/Logar", config)
            .then(function (dadosJson) {
                //console.log(dadosJson);
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                console.log(dadosObj);
                if (dadosObj.operacao)
                    window.location.href = "/HomeCli";
                else
                    document.getElementById("divMsg").innerHTML = dadosObj.msg;
            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "houve um erro";
            })

    }
}





