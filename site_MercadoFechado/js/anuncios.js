function CriarDivImagem() {
    var img = document.createElement("img");
    img.src = "images/PS5DigitalEdition.png";

    var containerImg = document.createElement("div");
    containerImg.classList.add("box-image");
    containerImg.append(img);
    return containerImg;
}

function CriarDivDetalhe(valorStr, descricao) {
    var divValor = document.createElement("div");
    divValor.classList.add("valor");
    divValor.innerText = valorStr;

    var divDescricao = document.createElement("div");
    divDescricao.classList.add("descricao");
    divDescricao.innerText = descricao;

    var containerDetalhes= document.createElement("div");
    containerDetalhes.classList.add("box-detalhe");
    containerDetalhes.append(divValor);
    containerDetalhes.append(divDescricao);
    return containerDetalhes;
}

const urlParams = new URLSearchParams(window.location.search);
const idCategoria = urlParams.get('idcategoria');
const nomeCategoria = urlParams.get('categoria');

document.getElementById("nome-categoria").innerText = nomeCategoria;

ObterAnuncios(idCategoria, (anuncios) => {
    var containerAnuncios = document.getElementById("anuncios-container");
    var formatter = new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL',
      
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
    });

    for(var i = 0; i < anuncios.length; i++) {
        var anuncio = anuncios[i];
        var containerImg = CriarDivImagem();
        var containerTexto = CriarDivDetalhe(formatter.format(anuncio.Valor), anuncio.Descricao);

        var box = document.createElement("a");
        box.classList.add("box");
        box.href = "anuncio.html?id_anuncio=" + anuncio.ID;
        console.log("setado!");
        box.append(containerImg);
        box.append(containerTexto);
        containerAnuncios.append(box);
    }
});