if(getCookie("token_acesso") != null && getCookie("token_acesso") != "") {
    var div = document.getElementById("botaologin");
    div.innerText = getCookie("usuario_logado");
    div.href = "perfil.html";
}