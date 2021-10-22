/* Lucas de Azevedo Juvito - 190047101
Mateus da Silva Souza   - 190035072 
*/

DROP DATABASE IF EXISTS mercado_fechado;
CREATE DATABASE mercado_fechado;
USE mercado_fechado;

CREATE TABLE usuario_comum(
    id_user_comum INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    login VARCHAR(50) NOT NULL UNIQUE,
    senha TINYTEXT NOT NULL
);

CREATE TABLE endereco(
    id_endereco INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    cep VARCHAR(9) NOT NULL,
    estado TINYTEXT NOT NULL,
    cidade TINYTEXT NOT NULL,
    bairro TINYTEXT,
    quadra TINYTEXT NOT NULL,
    numero INT NOT NULL,
    complemento TINYTEXT,
    id_proprietario INT NOT NULL,
    FOREIGN KEY (id_proprietario) REFERENCES usuario_comum(id_user_comum) ON UPDATE CASCADE
);

CREATE TABLE usuario_pes_juridica(
    id_pes_juridica INT NOT NULL PRIMARY KEY,
    cnpj VARCHAR(18) NOT NULL UNIQUE,
    endereco_sede INT NOT NULL UNIQUE,
    nome_fantasia TINYTEXT NOT NULL,
    FOREIGN KEY (id_pes_juridica) REFERENCES usuario_comum(id_user_comum) ON UPDATE CASCADE,
    FOREIGN KEY (endereco_sede) REFERENCES endereco(id_endereco) ON UPDATE CASCADE 
);

CREATE TABLE usuario_pes_fisica(
    id_pes_fisica INT NOT NULL PRIMARY KEY,
    cpf VARCHAR(14) NOT NULL UNIQUE,
    nome TINYTEXT NOT NULL,
    data_nascimento DATE NOT NULL,
    FOREIGN KEY (id_pes_fisica) REFERENCES usuario_comum(id_user_comum) ON UPDATE CASCADE
);

CREATE TABLE comentarios(
    id_comentario INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    descricao VARCHAR(300) NOT NULL,
    id_user_comum INT NOT NULL,
    id_anuncio INT NOT NULL,
    FOREIGN KEY (id_user_comum) REFERENCES usuario_comum(id_user_comum) ON UPDATE CASCADE,
    FOREIGN KEY (id_anuncio) REFERENCES anuncio(id_anuncio) ON UPDATE CASCADE
);

CREATE TABLE venda(
    id_venda INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    venda_hora DATETIME NOT NULL, 
    vendedor INT NOT NULL,
    comprador INT NOT NULL,
    valor INT NOT NULL,
    endereco_entrega INT NOT NULL,
    FOREIGN KEY (vendedor) REFERENCES usuario_comum(id_user_comum) ON UPDATE CASCADE,
    FOREIGN KEY (comprador) REFERENCES usuario_comum(id_user_comum) ON UPDATE CASCADE,
    FOREIGN KEY (endereco_entrega) REFERENCES endereco(id_endereco)
);

CREATE TABLE avaliacao(
    id_avaliacao INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    pontuacao SMALLINT NOT NULL,
    comentario TINYTEXT,
    id_user_comum INT NOT NULL,
    id_venda INT NOT NULL,
    FOREIGN KEY (id_user_comum) REFERENCES usuario_comum(id_user_comum) ON UPDATE CASCADE,
    FOREIGN KEY (id_venda) REFERENCES venda(id_venda) ON UPDATE CASCADE
);


CREATE TABLE categoria(
    id_categoria INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome TINYTEXT NOT NULL
);

CREATE TABLE produto(
    id_produto INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome TINYTEXT NOT NULL,
    marca TINYTEXT NOT NULL,
    fabricante TINYTEXT NOT NULL,
    ano_fabricacao SMALLINT NOT NULL,
    id_categoria INT NOT NULL,
    foto BLOB,
    descricao TINYTEXT,
    FOREIGN KEY (id_categoria) REFERENCES categoria(id_categoria) ON UPDATE CASCADE
);

CREATE TABLE anuncio(
    id_anuncio INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    titulo TINYTEXT NOT NULL,
    descricao TEXT NOT NULL,
    id_produto INT NOT NULL,
    id_vendedor INT NOT NULL,
    FOREIGN KEY (id_produto) REFERENCES produto(id_produto) ON UPDATE CASCADE,
    FOREIGN KEY (id_vendedor) REFERENCES usuario_comum(id_user_comum) ON UPDATE CASCADE
);

/* Usuarios */
insert into usuario_comum (login, senha) values ('user1','123');
insert into usuario_comum (login, senha) values ('user2','123');
insert into usuario_comum (login, senha) values ('user3','123');
insert into usuario_comum (login, senha) values ('user4','123');
insert into usuario_comum (login, senha) values ('user5','123');

/* Endereco */
insert into endereco (cep, estado, cidade, bairro, quadra, numero, id_proprietario) 
values ('72910-000', 'GO', 'Águas Lindas de Goiás', 'Perola 1', 'quadra 9', '900', 1);
insert into endereco (cep, estado, cidade, bairro, quadra, numero, id_proprietario) 
values ('72910-000', 'GO', 'Águas Lindas de Goiás', 'Perola 1', 'quadra 9', '9', 2);
insert into endereco (cep, estado, cidade, bairro, quadra, numero, id_proprietario) 
values ('72910-000', 'GO', 'Águas Lindas de Goiás', 'Perola 1', 'quadra 9', '1', 3);
insert into endereco (cep, estado, cidade, bairro, quadra, numero, id_proprietario) 
values ('72910-000', 'GO', 'Águas Lindas de Goiás', 'Perola 1', 'quadra 9', '2', 4);
insert into endereco (cep, estado, cidade, bairro, quadra, numero, id_proprietario) 
values ('72910-000', 'GO', 'Águas Lindas de Goiás', 'Perola 1', 'quadra 9', '5', 5);

/* Pessoas Juridicas */
insert into usuario_pes_juridica (id_pes_juridica, cnpj, endereco_sede, nome_fantasia) 
values (1, '70.450.542/0001-41', 1, 'Amezon');
insert into usuario_pes_juridica (id_pes_juridica, cnpj, endereco_sede, nome_fantasia) 
values (2, '70.450.542/0001-42', 2, 'Mercado Socialista');

/* Pessoas Fisicas */
insert into usuario_pes_fisica (id_pes_fisica, cpf, nome, data_nascimento) 
values (3, '598.736.450-71', 'Hulk', current_date());
insert into usuario_pes_fisica (id_pes_fisica, cpf, nome, data_nascimento) 
values (4, '908.531.950-13', 'Batman', current_date());
insert into usuario_pes_fisica (id_pes_fisica, cpf, nome, data_nascimento) 
values (5, '115.717.090-02', 'Arlequina', current_date());

/* Categoria */
insert into categoria (nome) values ('CONSOLES');
insert into categoria (nome) values ('COMPONENTES PARA PC');
insert into categoria (nome) values ('LIVROS');
insert into categoria (nome) values ('FILMES');
insert into categoria (nome) values ('CELULARES');

/* produtos */
insert into produto (nome, marca, fabricante, ano_fabricacao, id_categoria, descricao) 
values ('Playstation 5', 'SONY', 'SONY', 2020, 1, 'cor: branco');
insert into produto (nome, marca, fabricante, ano_fabricacao, id_categoria, descricao) 
values ('RTX 3090', 'NVIDIA', 'ZOTAC', 2020, 2, 'cor: preta, led: sim');
insert into produto (nome, marca, fabricante, ano_fabricacao, id_categoria, descricao) 
values ('Reinos de Runeterra', 'LEAGUE OF LEGENDS', 'RIOT', 2020, 3, 'capa: dura');
insert into produto (nome, marca, fabricante, ano_fabricacao, id_categoria, descricao) 
values ('Homem Aranha 2', 'SONY', 'SONY', 2004, 4, 'dvd');
insert into produto (nome, marca, fabricante, ano_fabricacao, id_categoria, descricao) 
values ('Iphone 13', 'APPLE', 'APPLE', 2021, 5, 'cor: azul cierra');

/* anuncio */
insert into anuncio (titulo, descricao, id_produto, id_vendedor)
values ('PS5 muito barato', 'O console mais potente da útima geração', 1, 1);
insert into anuncio (titulo, descricao, id_produto, id_vendedor)
values ('Placa de vídeo para turbinar os seus video jogos', 'A melhor placa de vídeo já feita para os meros mortais \nPS: não me responsabilizo por problemas', 2, 2);
insert into anuncio (titulo, descricao, id_produto, id_vendedor)
values ('Livro do LOL', 'LOL LOL LOL', 3, 3);
insert into anuncio (titulo, descricao, id_produto, id_vendedor)
values ('DVD antigo do Homem Aranha 2', 'Edição de colecionador rabiscada pelo diretor do filme e talvez meus filhos \nPS: talvez não tenha sido rabiscada pelo diretor', 4, 4);
insert into anuncio (titulo, descricao, id_produto, id_vendedor)
values ('IPHONE 13 único no Brazil', 'Mais atual celular android que você terá em suas mãos', 5, 5);

/* comentario */
/* avaliacao */
/* venda */