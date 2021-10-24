//CriarAvaliacao(getCookie("token_acesso"), vendaId, divAvaliacao.value, divDescricao.value, (resposta)
function CriarAvaliacao(token, id_venda, avaliacao, comentario, onsuccess, onerror) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "http://localhost:1890/avaliacoes/criar", true);
    xhr.onreadystatechange = function () {
        if (this.readyState != 4) return;

        if (this.status == 200) {
            var data = JSON.parse(this.responseText);
            console.log(data);
            onsuccess(data);
        }
    };
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.setRequestHeader('Authorization', token);
    xhr.send(JSON.stringify({
        "IDVenda" : parseInt(id_venda),
        "Pontuacao" : parseFloat(avaliacao),
        "Comentario": comentario
    }));
}

function DeletarAvaliacao(token, id_venda, onsuccess, onerror) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "http://localhost:1890/avaliacoes/deletar", true);
    xhr.onreadystatechange = function () {
        if (this.readyState != 4) return;

        if (this.status == 200) {
            var data = JSON.parse(this.responseText);
            console.log(data);
            onsuccess(data);
        }
    };
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.setRequestHeader('Authorization', token);
    xhr.send(JSON.stringify({
        "ID" : parseInt(id_venda)
    }));
}