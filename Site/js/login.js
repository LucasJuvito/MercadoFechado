function TentarLogar() {
    var divLogin = document.getElementById("nome_login");
    var divSenha = document.getElementById("senha_login");

    var valido = true;
    if(divLogin.value == null || divLogin.value.length < 1) {
        divLogin.reportValidity();
        valido = false;
    }

    if(divSenha.value == null || divSenha.value.length < 1) {
        divSenha.reportValidity();
        valido = false;
    }

    if(!valido)
        return;

    var usuario = document.getElementById("nome_login").value;
    var senha = document.getElementById("senha_login").value;

    LogarUsuario(usuario, senha,
        (dados) => {
            setCookie("usuario_logado", usuario, 1);
            setCookie("token_acesso", dados.Token, 1);
            setCookie("id_usuario_logado", dados.ID, 1);
            document.location = "../index.html";
        },
        (erro) => {
            document.getElementById("erro_login").innerText = erro.Message;
            divLogin.classList = "invalido";
            divSenha.classList = "invalido";
        }
    );
}