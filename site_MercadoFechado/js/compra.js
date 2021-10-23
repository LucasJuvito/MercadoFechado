const urlParams = new URLSearchParams(window.location.search);
const idAnuncio = urlParams.get('id_anuncio');
var temSelecionado = false;
var idSelecionado;

function SelecionarDiv(id) {
    if(temSelecionado)
        document.getElementById("endereco-" + idSelecionado).classList.replace("selecionado", "&nbsp;");
    
    temSelecionado = true;
    document.getElementById("endereco-" + id).classList = "endereco selecionado";
    idSelecionado = id;
}

function FinalizarCompra() {
    EfetuarCompra(getCookie("token_acesso"), idAnuncio, idSelecionado, (resultado) => {
        alert("Sucesso!");
        document.location = "./minhas-compras.html";
    });
}

ObterDetalhes(idAnuncio, (detalhes) => {
    var formatter = new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL',
      
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
    });
    
    document.getElementById("anuncio-nome").innerText = detalhes.Titulo;
    //document.getElementById("texto-descricao").innerText = detalhes.Descricao;
    document.getElementById("valor").innerText = formatter.format(detalhes.Valor);

    ObterEnderecos(getCookie("token_acesso"), (enderecos) => {
        var container = document.getElementById("selecionar-endereco");

        for(var i = 0; i < enderecos.length; i++) {
            var endereco = enderecos[i];
            var div = CriarDivEndereco(endereco);
            container.innerHTML += div;
            SelecionarDiv(endereco.ID);
        }        
        console.log(enderecos);
    });

    ObterDadosUsuario(detalhes.IDVendedor, (detalhesUsuario) => {
        document.getElementById("vendedor-nome").innerText = detalhesUsuario.Nome;
    });

    ObterDetalhesProduto(detalhes.IDProduto, (detalhesProduto) => {
        document.getElementById("produto-nome").innerText = detalhesProduto.Nome;
        console.log(detalhesProduto);
    });
});