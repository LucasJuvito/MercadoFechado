function replaceAll(str, find, replace) {
    return str.replace(new RegExp(find, 'g'), replace);
}

function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays*24*60*60*1000));
    let expires = "expires="+ d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    console.log(decodedCookie);
    let ca = decodedCookie.split(';');
    for(let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return null;
}

function GerarEstrelinhas(pontuacao) {
    var qntEstrelas = 0;
    var ret = "";

    while(pontuacao - 1 >= 0) {
        ret += '<i class="fas fa-star"></i>';
        qntEstrelas++;
        pontuacao--;
    }

    if(pontuacao > 0) {
        ret += '<i class="fas fa-star-half-alt"></i>';
        qntEstrelas++;
    }

    while(qntEstrelas < 5) {
        ret += '<i class="far fa-star"></i>';
        qntEstrelas++;
    }
    return ret;
}