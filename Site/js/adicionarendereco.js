function ClicarAdicionarEndereco() {
    var cep = document.getElementById("cep_cad").value;
    var estado = document.getElementById("estado_cad").value;
    var cidade = document.getElementById("cidade_cad").value;
    var bairro = document.getElementById("bairro_cad").value;
    var quadra = document.getElementById("quadra_cad").value;
    var numeroStr = document.getElementById("numero_cad").value;
    var complemento = document.getElementById("complemento_cad").value;

    AdicionarEndereco(getCookie("token_acesso"), {
        CEP: cep,
        Estado: estado,
        Cidade: cidade,
        Bairro: bairro,
        Quadra: quadra,
        Numero: parseInt(numeroStr),
        complemento: complemento
    },
    (dados) => {
        alert("Endereço cadastrado com sucesso");
        document.location = "./perfil.html";
    });
    console.log("teste");
}

ObterEstados((estados) => {
    for(var i = 0; i < estados.length; i++) {
        var estado = estados[i];
        var baseStr = '<option value="%sigla%">%estado%</option>';
        baseStr = baseStr.replace("%sigla%", estado.Sigla);
        baseStr = baseStr.replace("%estado%", estado.Descricao);
        document.getElementById("estado_cad").innerHTML += baseStr;

        console.log(estados);
    }
});