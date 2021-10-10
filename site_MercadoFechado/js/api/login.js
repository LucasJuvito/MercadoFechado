function ObterToken(usuario, senha, onsuccess, onerror) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "http://localhost:1890/login/logar", true);
    xhr.onreadystatechange = function () {
        if (this.readyState != 4) return;

        console.log(this.responseText);
        var data = JSON.parse(this.responseText);
        if (this.status == 200) {
            if(data.Success)
                onsuccess(data.Token);
            else
                onerror(data);
        }
    };
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify({
        "Usuario" : usuario,
        "Senha": senha
    }));
}