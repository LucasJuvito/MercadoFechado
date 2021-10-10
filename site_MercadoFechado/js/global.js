if(getCookie("token_acesso") != null) {
    var div = document.getElementById("botaologin");
    div.innerText = getCookie("usuario_logado");
    div.href = "perfil.html";
}