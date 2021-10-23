function ClicarDeslogar() {
    DeslogarUsuario(getCookie("token_acesso"), (resposta) => {
        alert("Deslogado com sucesso!");
        setCookie("token_acesso", "");
        setCookie("usuario_logado", "");
        document.location = "./index.html";
    });
}