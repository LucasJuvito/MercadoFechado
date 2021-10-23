var temSelecionado = false;
var idSelecionado;

function SelecionarDiv(id) {
    if(temSelecionado) {
        console.log()
        console.log(document.getElementById("produto-" + idSelecionado));
        document.getElementById("produto-" + idSelecionado).classList.replace("selecionado", "&nbsp;");
    }
    
    temSelecionado = true;
    document.getElementById("produto-" + id).classList = "produto selecionado";
    idSelecionado = id;
}

function ProximoPasso() {
    if(!temSelecionado) {
        alert("Você não selecionou nenhum produto!");
        return;
    }

    document.location = "./vender.html?id_produto=" + idSelecionado + "#cadastrar-anuncio";
}

function CriarDisplayProduto(produto) {
    var baseStr = `
    <div id="id_produto" class="produto" onclick="SelecionarDiv(%idclick%)">
        <div>nome</div>
        <div>marca</div>
        <div>fabricante</div>
        <div>anofabricacao</div>
        <div>descricao</div>
    </div>`;
    baseStr = baseStr.replace("nome", produto.Nome);
    baseStr = baseStr.replace("marca", produto.Marca);
    baseStr = baseStr.replace("fabricante", produto.Fabricante);
    baseStr = baseStr.replace("anofabricacao", produto.Ano);
    baseStr = baseStr.replace("descricao", produto.Descricao);
    baseStr = baseStr.replace("id_produto", "produto-" + produto.ID);
    baseStr = baseStr.replace("%idclick%", produto.ID);
    return baseStr;
}

function AoMudarCategoria() {
    var divCategorias = document.getElementById("selecao-categoria");
    temSelecionado = false;
    if(divCategorias.value == "none") {
        console.log("kaka");
        document.getElementById("lista").innerHTML = "";
        return;
    }
    
    ListarProdutosPorCategoria(divCategorias.value, (dados) => {
        var divContainer = document.getElementById("lista");
        var produtos = dados.Produtos;
        divContainer.innerHTML = "";
        console.log(dados);
        for(var i = 0; i < produtos.length; i++) {
            var produto = produtos[i];
            var div = CriarDisplayProduto(produto);
            divContainer.innerHTML += div;
        }
    });
}

ObterCategorias((categorias) => {
    var divCategorias = document.getElementById("selecao-categoria");
    var base = '<option value="%id_categoria%">%nome_categoria%</option>';
    for(var i = 0; i < categorias.length; i++) {
        var elemento = replaceAll(base, "%id_categoria%", categorias[i].ID);
        elemento = replaceAll(elemento, "%nome_categoria%", categorias[i].Nome);
        divCategorias.innerHTML += elemento;
    }
});

AoMudarCategoria();