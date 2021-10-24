const urlParams = new URLSearchParams(window.location.search);
const idAnuncio = urlParams.get('id_anuncio');
var idUsuarioLogado = getCookie("id_usuario_logado");

function ClicarDeletarComentario(id) {
    DeletarComentario(getCookie("token_acesso"), id, (resposta) => {
        alert("Comentário deletado com sucesso!");
        document.location.reload();
    });
}

function CriarDivContainer(idcomentario, idautor, nomeautor, comentario) {
    var divAutor = document.createElement("div");
    divAutor.classList.add("autor");

    if(idUsuarioLogado == idautor)
        divAutor.innerHTML = nomeautor + '<button class="btn-delete" onclick="ClicarDeletarComentario(' + idcomentario + ')">Deletar</button>';
    else 
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

function ClicarSalvarDescricao() {
    var novaDescricao = document.getElementById("texto-descricao").innerText;
    AtualizarDescricaoAnuncio(getCookie("token_acesso"), idAnuncio, novaDescricao, (dados) => {
        alert(dados.Message);
    });
}

function ClicarSalvarTitulo() {
    var novoTitulo = document.getElementById("titulo").innerText;
    AtualizarTituloAnuncio(getCookie("token_acesso"), idAnuncio, novoTitulo, (dados) => {
        if(dados.Success) {
            document.getElementById("titulo").classList = "titulo";
            document.getElementById("titulo").contentEditable = false;
            document.getElementById("btn-editar-titulo").innerText = "Editar";
            document.getElementById("btn-editar-titulo").onclick = ClicarEditarTitulo;
        }
        alert(dados.Message);
    });
}

function ClicarEditarTitulo() {
    document.getElementById("titulo").classList = "titulo titulo-editavel";
    document.getElementById("titulo").contentEditable = true;
    document.getElementById("btn-editar-titulo").innerText = "Salvar";
    document.getElementById("btn-editar-titulo").onclick = ClicarSalvarTitulo;
}

function ClicarEditarDescricao() {
    document.getElementById("texto-descricao").classList = "texto-descricao texto-descricao-editavel";
    document.getElementById("texto-descricao").contentEditable = true;
    document.getElementById("btn-editar-descricao").innerText = "Salvar";
    document.getElementById("btn-editar-descricao").onclick = ClicarSalvarDescricao;
}

function ClicarAdicionarComentario() {
    var texto = document.getElementById("texto-comentario").value;
    AdicionarComentario(getCookie("token_acesso"), idAnuncio, texto, (resposta) => {
        alert(resposta.Message);
        window.location.reload();
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

    if(getCookie("id_usuario_logado") != null && detalhes.IDVendedor == parseInt(getCookie("id_usuario_logado"))) {
        document.getElementById("btn-editar-titulo").style.display = 'block';
        document.getElementById("btn-editar-descricao").style.display = 'block';
    }

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
            var container = CriarDivContainer(comentario.ID, comentario.IDUserComum, dados.Nome, comentario.Descricao);
            containerComentarios.append(container);
        });
    }
});

ObterAvaliacoes(idAnuncio, (avaliacoes) => {
    var containerAvaliacoes = document.getElementById("avaliacoes");
    for(var i = 0; i < avaliacoes.length; i++) {
        var avaliacao = avaliacoes[i];
        var container = CriarDivContainer(0, 0, GerarEstrelinhas(avaliacao.Pontuacao), avaliacao.Comentario);
        containerAvaliacoes.append(container);
    }
});