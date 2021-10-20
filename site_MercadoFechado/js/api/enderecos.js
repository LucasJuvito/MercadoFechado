function ObterEnderecos(token, onsuccess, onerror) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "http://localhost:1890/enderecos/listar", true);
    xhr.onreadystatechange = function () {
        if (this.readyState != 4) return;

        if (this.status == 200) {
            var data = JSON.parse(this.responseText);
            console.log(data);
            onsuccess(data.Enderecos);
        }
    };
    xhr.setRequestHeader('Authorization', token);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send();
}