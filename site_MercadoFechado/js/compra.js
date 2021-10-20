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

function CriarDivEndereco(endereco) {
    var baseStr = `
        <div id="endereco-%id%" class="endereco" onclick="SelecionarDiv(%idclick%)">
            <div>%cep%</div>
            <div>%estado%</div>
            <div>%cidade%</div>
            <div>%bairro%</div>
            <div>%quadra%</div>
            <div>%numero%</div>
            <div>%complemento%</div>
        </div>
    `;

    baseStr = baseStr.replace("%id%", endereco.ID);
    baseStr = baseStr.replace("%cep%", endereco.CEP);
    baseStr = baseStr.replace("%estado%", endereco.SiglaEstado);
    baseStr = baseStr.replace("%cidade%", endereco.Cidade);
    baseStr = baseStr.replace("%bairro%", endereco.Bairro);
    baseStr = baseStr.replace("%quadra%", endereco.Quadra);
    baseStr = baseStr.replace("%numero%", endereco.Numero);
    baseStr = baseStr.replace("%idclick%", endereco.ID);
    baseStr = baseStr.replace("%complemento%", endereco.Complemento ? endereco.Complemento : "");
    return baseStr;
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