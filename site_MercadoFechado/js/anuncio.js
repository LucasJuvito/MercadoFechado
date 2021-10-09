const urlParams = new URLSearchParams(window.location.search);
const idAnuncio = urlParams.get('id_anuncio');

/*
<div class="comentario">
    <div class="autor">Autor aleaório</div>
    <div class="coment"> medo de levantar. Já apostei em correr descalço na rua, Já gritei de felicidade, Já roubei rosas num enorme jardim. Já me apaixonei e achei que era para sempre, Mas sempre era um "para sempre" pela metade. Já deitei na grama de madrugada E vi a Lua virar Sol, Já chorei por ver amigos partindo, Mas descobri que logo chegam novos, e a vida é mesmo um ir e vir sem razão. Foram tantas coisas feitas, Momentos fotografados pelas lentes da emoção. Guardados num baú, chamado coração. E agora um formulário me interroga, Encosta-me na parede e grita: "- Qual sua experiência?”. Essa pergunta ecoa no meu cérebro: "experiência... experiência...” Será que ser "plantador de sorrisos" é uma boa experiência? Não! ”Talvez eles não saibam ainda colher sonhos!”</div>
</div>
*/

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

function GerarEstrelinhas(pontuacao) {
    var qntEstrelas = 0;
    var ret = "";

    while(pontuacao - 1 >= 0) {
        ret += '<i class="fas fa-star"></i>';
        qntEstrelas++;
        pontuacao--;
    }

    if(pontuacao > 0) {
        ret += '<i class="fas fa-star-half-alt"></i>';
        qntEstrelas++;
    }

    while(qntEstrelas < 5) {
        ret += '<i class="far fa-star"></i>';
        qntEstrelas++;
    }
    return ret;
}

ObterDetalhes(idAnuncio, (detalhes) => {
    var formatter = new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL',
      
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
    });
    
    document.getElementById("titulo").innerText = detalhes.Titulo;
    document.getElementById("texto-descricao").innerText = detalhes.Descricao;
    document.getElementById("preco-produto").innerText = formatter.format(detalhes.Valor);
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