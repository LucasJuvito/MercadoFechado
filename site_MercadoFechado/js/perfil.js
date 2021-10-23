function ClicarDeslogar() {
    DeslogarUsuario(getCookie("token_acesso"), (resposta) => {
        console.log(resposta);
    });
}

ObterDadosUsuarioLogado(getCookie("token_acesso"), (dados) => {
    document.getElementById("nome-pessoa").innerText = dados.Nome;
    document.getElementById("sigla-identificador").innerText = dados.TipoPessoa == 1 ? "CPF:" : "CNPJ:";
    document.getElementById("identificador").innerText = dados.Identificador;
    document.getElementById("usuario-logado").innerText = getCookie("usuario_logado");
    console.log(dados);
});

ObterEnderecos(getCookie("token_acesso"), (enderecos) => {
    var container = document.getElementById("enderecos");

    for(var i = 0; i < enderecos.length; i++) {
        var endereco = enderecos[i];
        var div = CriarDivEndereco(endereco);
        container.innerHTML = div + container.innerHTML;
    }        
    console.log(enderecos);
});