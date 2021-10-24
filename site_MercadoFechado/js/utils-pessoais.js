function ClicarDeslogar() {
    DeslogarUsuario(getCookie("token_acesso"), (resposta) => {
        alert("Deslogado com sucesso!");
        setCookie("token_acesso", "");
        setCookie("usuario_logado", "");
        setCookie("id_usuario_logado", "-1");
        document.location = "./index.html";
    });
}