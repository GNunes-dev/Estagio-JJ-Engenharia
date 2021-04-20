//padrão módulo para organizar o código
let index = {
    buscarParametro: function () {

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Parametro/obterParametro", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {
                console.log(dadosObj);
                if (!dadosObj)
                    window.location.href = "/Parametro/Index";

            })
            .catch(function () {
            })

    },
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

        fetch("/LoginAdm/Logar", config)
            .then(function (dadosJson) {
                //console.log(dadosJson);
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                console.log(dadosObj);
                if (dadosObj.operacao)
                    window.location.href = "/HomeADM";
                else
                    document.getElementById("divMsg").innerHTML = dadosObj.msg;
            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "houve um erro";
            })

    }
   
}

index.buscarParametro();



