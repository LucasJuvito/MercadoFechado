function ObterCategorias(onsuccess, onerror) {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "http://localhost:1890/categorias/listar", true);
    xhr.onreadystatechange = function () {
        if (this.readyState != 4) return;

        if (this.status == 200) {
            var data = JSON.parse(this.responseText);
            onsuccess(data.Categorias);
        }
    };
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send();
}