const urlParams = new URLSearchParams(window.location.search);
const vendaId = urlParams.get('id_venda');

function ClicarAvaliar() {
    var divAvaliacao = document.getElementById("avaliacao");
    var divDescricao = document.getElementById("texto-comentario");

    CriarAvaliacao(getCookie("token_acesso"), vendaId, divAvaliacao.value, divDescricao.value, (resposta) => {
        alert(resposta.Message);
        window.location = "./minhas-compras.html";
    });
}