function EfetuarCompra(token, idanuncio, idendereco, onsuccess, onerror) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "http://localhost:1890/vendas/comprar", false);
    xhr.onreadystatechange = function () {
        if (this.readyState != 4) return;

        if (this.status == 200) {
            var data = JSON.parse(this.responseText);
            onsuccess(data);
        }
    };
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.setRequestHeader('Authorization', token);

    xhr.send(JSON.stringify({
        "IDAnuncio" : parseInt(idanuncio),
        "IDEndereco" : parseInt(idendereco)
    }));
}