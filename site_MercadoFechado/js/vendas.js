function CriarDivVenda(venda) {
    var baseStr = `<div class="compra">
        <div class="titulo">%titulo_anuncio%</div>
        <div class="content">
            <div class="coluna1">
                <img src="http://localhost:1890/produtos/buscar/imagem?id_produto=%id_produto%">
            </div>

            <div class="coluna2">
                <div class="data">
                    <h1>Data da compra:</h1>
                    <div class="data-compra">%data_compra%</div>
                </div>
                <div class="vendedor">
                    <h1>Comprador:</h1>
                    <div class="vendedor-nome">%nome_comprador%</div>
                </div>
                <div class="avaliacao">
                    <h1>Avaliação do cliente:</h1>
                    <div class="valor">%avaliacao%</div>
                </div>
            </div>
        </div>
    </div>`;
    baseStr = baseStr.replace("%id_produto%", venda.IDProduto);
    baseStr = baseStr.replace("%data_compra%", venda.HorarioVenda);
    baseStr = baseStr.replace("%titulo_anuncio%", venda.Titulo);

    var pontuacao = parseInt(venda.Pontuacao);
    if(pontuacao >= 0)
        baseStr = baseStr.replace("%avaliacao%", GerarEstrelinhas(pontuacao));
    else
        baseStr = baseStr.replace("%avaliacao%", 'Ainda não avaliado');
    return baseStr;
}

ObterVendasUsuarioLogado(getCookie("token_acesso"), (dados) => {
    for(var i = 0; i < dados.Vendas.length; i++) {
        var venda = dados.Vendas[i];
        var div = CriarDivVenda(venda);

        ObterDadosUsuario(venda.IDComprador, (dadosUsuario) => {
            div = div.replace("%nome_comprador%", dadosUsuario.Nome);
            document.getElementById("minhas-compras-container").innerHTML += div;
        });
    }
    console.log(dados);
});