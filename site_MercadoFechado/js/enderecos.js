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