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

ObterCategorias((categorias) => {
    var divCategorias = document.getElementById("categorias-lista");
    var base = '<a href="anuncios.html?idcategoria=id_categoria&categoria=nome_categoria"><button class="btn_categorias" id="id_categoria">nome_categoria</button></a>';
    for(var i = 0; i < categorias.length; i++) {
        var elemento = replaceAll(base, "id_categoria", categorias[i].ID);
        elemento = replaceAll(elemento, "nome_categoria", categorias[i].Nome);
        divCategorias.innerHTML += elemento;
    }
});