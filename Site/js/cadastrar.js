function CadastrarPessoaFisica() {
    var divNome = document.getElementById("nome_cad");
    var divCPF = document.getElementById("cpf_cad");
    var divLogin = document.getElementById("nome_user_cad");
    var divSenha = document.getElementById("senha_cad");

    CriarUsuarioPessoaFisica(divLogin.value, divSenha.value, divCPF.value, divNome.value, '2021-07-21', (resposta) => {
        console.log(resposta)
        if(resposta.Success) {
            alert("Conta criada com sucesso!");
            document.location = "./login.html#login";
        }
        else {
            alert(resposta.Message);
        }
    });
}

function CadastrarPessoaJuridica() {
    var divNome = document.getElementById("nome_fantasia_cad");
    var divCPF = document.getElementById("cnpj_cad");
    var divLogin = document.getElementById("nome_user_juridico_cad");
    var divSenha = document.getElementById("senha_juridico_cad");

    CriarUsuarioPessoaJuridica(divLogin.value, divSenha.value, divCPF.value, divNome.value, (resposta) => {
        console.log(resposta)
        if(resposta.Success) {
            alert("Conta criada com sucesso!");
            document.location = "./login.html#login";
        }
        else {
            alert(resposta.Message);
        }
    });
}

/*
<p> 
          <label for="nome_fantasia_cad">Nome Fantasia</label>
          <input id="nome_fantasia_cad" name="nome_cad" required="required" type="text" placeholder="nome" />
        </p>
          
        <p> 
          <label for="cnpj_cad">Seu CNPJ</label>
          <input id="cnpj_cad" pattern="\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}" title="Digite um CNPJ no formato: xx.xxx.xxx/xxxx-xx" name="cpf_cad" required="required" type="text" placeholder="00.000.000/0000-00"/> 
        </p>

        <p> 
          <label for="nome_user_juridico_cad">Seu nome de usuário</label>
          <input id="nome_user_juridico_cad" name="nome_user_cad" required="required" type="text" placeholder="nome" />
        </p>
          
        <p> 
          <label for="senha_juridico_cad">Sua senha</label>
          <input id="senha_juridico_cad" name="senha_cad" required="required" type="password" placeholder="ex. 1234"/>
        </p>*/