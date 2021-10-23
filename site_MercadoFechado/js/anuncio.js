const urlParams = new URLSearchParams(window.location.search);
const idAnuncio = urlParams.get('id_anuncio');

function CriarDivContainer(nomeautor, comentario) {
    var divAutor = document.createElement("div");
    divAutor.classList.add("autor");
    divAutor.innerHTML = nomeautor;

    var divComentario = document.createElement("div");
    divComentario.classList.add("coment");
    divComentario.innerText = comentario;

    var container = document.createElement("div");
    container.classList.add("comentario");
    container.append(divAutor);
    container.append(divComentario);
    return container;
}

function ClicarAdicionarComentario() {
    var texto = document.getElementById("texto-comentario").value;
    AdicionarComentario(getCookie("token_acesso"), idAnuncio, texto, (resposta) => {
        console.log(resposta);
    });
    return container;
}

ObterDetalhes(idAnuncio, (detalhes) => {
    var formatter = new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL',
      
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
    });
    
    document.getElementById("btn_comprar").href = "./compra.html?id_anuncio=" + idAnuncio;
    document.getElementById("titulo").innerText = detalhes.Titulo;
    document.getElementById("texto-descricao").innerText = detalhes.Descricao;
    document.getElementById("preco-produto").innerText = formatter.format(detalhes.Valor);
    document.getElementById("img-produto").src = "http://localhost:1890/produtos/buscar/imagem?id_produto=" + detalhes.IDProduto;
    console.log(detalhes);
});

ObterComentarios(idAnuncio, (comentarios) => {
    var containerComentarios = document.getElementById("comentarios");
    for(var i = 0; i < comentarios.length; i++) {
        var comentario = comentarios[i];
        ObterDadosUsuario(comentario.IDUserComum, (dados) => {
            var container = CriarDivContainer(dados.Nome, comentario.Descricao);
            containerComentarios.append(container);
        });
    }
});

ObterAvaliacoes(idAnuncio, (avaliacoes) => {
    var containerAvaliacoes = document.getElementById("avaliacoes");
    for(var i = 0; i < avaliacoes.length; i++) {
        var avaliacao = avaliacoes[i];
        var container = CriarDivContainer(GerarEstrelinhas(avaliacao.Pontuacao), avaliacao.Comentario);
        containerAvaliacoes.append(container);
    }
});