if(getCookie("token_acesso") != null && getCookie("token_acesso") != "") {
    ObterDadosUsuarioLogado(getCookie("token_acesso"), (dados) => {
        if(!dados.Success) {
            return;
        }

        var divUsuario = document.getElementById("botaologin");
    
        divUsuario.innerText = dados.Login;
        divUsuario.href = "perfil.html";

        var formatter = new Intl.NumberFormat('pt-BR', {
            style: 'currency',
            currency: 'BRL',
          
            minimumFractionDigits: 2,
            maximumFractionDigits: 2,
        });

        var divSaldo = document.getElementById("saldo");
        
        divSaldo.style.display = 'block';
        divSaldo.innerText = formatter.format(parseFloat(dados.Saldo)).toString();
    });
}